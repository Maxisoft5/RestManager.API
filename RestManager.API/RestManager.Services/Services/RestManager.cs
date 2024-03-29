﻿using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.DataAccess.Models.Enums;
using RestManager.DataAccess.Repositories.Interfaces;
using RestManager.Services.Extensions;
using RestManager.Services.Interfaces;
using RestManager.Services.ModelDTO;
using RestManager.Services.Queues;
using RestManager.Services.Records;

namespace RestManager.Services.Services
{
    public class RestManager : IRestManager
    {

        private readonly CheckForFreeTableQueue _checkForFreeTableQueue;
        private readonly IRestManagerRepository _managerRepository;
        private readonly IMapper _mapper;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public RestManager(IRestManagerRepository managerRepository, CheckForFreeTableQueue checkForFreeTableQueue, IMapper mapper)
        {
            _checkForFreeTableQueue = checkForFreeTableQueue;
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<SetClientsResult> SetClientsToTable(ClientGroupDTO clientGroup,
            long restorantId, long tableId)
        {
            semaphoreSlim.Wait();
            var restorant = await _managerRepository.GetRestorant(restorantId);
            if (restorant == null)
            {
                semaphoreSlim.Release();
                return SetClientsResult.NotFoundRestorant(restorantId, clientGroup, tableId);
            }
            clientGroup.RestorantId = restorantId;
            var isGroupExists = await _managerRepository.CheckIfGroupAlreadyExists(clientGroup.Id);
            if (!isGroupExists)
            {
                var group = await AddClientGroup(clientGroup, restorantId);
                clientGroup.Id = group.Id;
            }
            var requestDTO = new TableRequestDTO()
            {
                WhenGroupSetAtTableDateTime = DateTime.UtcNow,
                PlacesToTakeCount = clientGroup.Clients.Count(),
                RequestDateTime = DateTime.UtcNow,
                RequestTableStatus = RequestTableStatus.GroupIsAtTable,
                TableId = tableId,
                ClientGroupId = clientGroup.Id
            };

            if (await EnsureAvaibleTablesExists(restorantId, clientGroup.Clients.Count()))
            {
                var request = _mapper.Map<TableRequest>(requestDTO);
                var savedRequest = await _managerRepository.AddTableRequest(request);
                semaphoreSlim.Release();
                return SetClientsResult.SuccessfullyAdded(restorantId, clientGroup, tableId);
            }
            else
            {
                requestDTO.RequestTableStatus = RequestTableStatus.GroupInQueue;
                var request = _mapper.Map<TableRequest>(requestDTO);
                var savedRequest = await _managerRepository.AddTableRequest(request);
                var result = await QueueForNextAvaibleTable(clientGroup, restorantId);
                if (result.IsError)
                {
                    semaphoreSlim.Release();
                    return SetClientsResult.UnExcpectedErrorWhenTryToSetToQueue(restorantId, clientGroup, tableId, result.Message);
                }
                semaphoreSlim.Release();
                return SetClientsResult.GroupWasSentToQueue(restorantId, clientGroup, tableId);
            }
            

        }

        private async Task<bool> EnsureAvaibleTablesExists(long restorantId, int clientsCount)
        {
            var tables = await GetAvaibleTablesInRestorant(restorantId, clientsCount);
            return tables.Tables.Count() > 0;
        }

        public async Task<SearchAvaibleTableResult> GetAvaibleTablesInRestorant(long restorantId,
            int clientsCount)
        {
            var restorant = await _managerRepository.GetRestorant(restorantId);
            if (restorant == null)
            {
                return SearchAvaibleTableResult.NotFoundRestorant(restorantId);
            }
            var fullyFree = restorant.Tables.FullyFree(clientsCount);
            if (fullyFree.Any())
            {
                var dto = _mapper.Map<IEnumerable<TableDTO>>(fullyFree);
                return SearchAvaibleTableResult.SuccessfullyFound(restorantId, dto);
            }

            var notFullyButHaveSpace = restorant.Tables.NotFullyFreeButHaveSpace(clientsCount);

            if (notFullyButHaveSpace.Any())
            {
                var dto = _mapper.Map<IEnumerable<TableDTO>>(notFullyButHaveSpace);
                return SearchAvaibleTableResult.SuccessfullyFound(restorantId, dto);
            }

            return SearchAvaibleTableResult.NotFoundTables(restorantId);
        }

        public async Task<QueueForNextTableResult> QueueForNextAvaibleTable(ClientGroupDTO clientGroup,
            long restorantId)
        {
            var restorant = await _managerRepository.GetRestorant(restorantId);
            if (restorant == null)
            {
                return QueueForNextTableResult.NotFoundRestorant(restorantId);
            }
            clientGroup.RestorantId = restorantId;
            var group = await AddClientGroup(clientGroup, restorantId);

            var queueDto = new QueueForTableDTO()
            {
                ClientGroupId = group.Id,
                QueueForTableStatus = QueueForTableStatus.ToProcessed
            };
            var queue = _mapper.Map<QueueForTable>(queueDto);
            var addedQueue = await _managerRepository.AddQueue(queue);
            _checkForFreeTableQueue.QueueBackgroundWorkItem(async (token) =>
            {

            });

            return QueueForNextTableResult.SuccessfullyAdded(restorantId, group.Id, addedQueue.Id);
        }

        public async Task<ClientGroupDTO> AddClientGroup(ClientGroupDTO clientGroup, long restorantId)
        {
            var group = _mapper.Map<ClientGroup>(clientGroup);
            group.Restorant = null; // avoid adding new entries
            group.TableRequests = null; //
            group.Clients = null;

            clientGroup.RestorantId = restorantId;
            var addedGroup = await _managerRepository.AddClientGroup(group);

            var clients = _mapper.Map<IEnumerable<Client>>(clientGroup.Clients);
            foreach (var client in clients)
            {
                client.GroupId = addedGroup.Id;
            }
            var savedClients = await _managerRepository.AddClients(clients);
            addedGroup.Clients = savedClients;
            return _mapper.Map<ClientGroupDTO>(addedGroup);
        }

        public async Task ProccessQueues()
        {
            IEnumerable<QueueForTable> queues = await _managerRepository.GetToProccessQueues();
            while (queues.Any())
            {
                foreach (var queue in queues)
                {
                    if (queue.ClientGroup == null)
                    {
                        queue.ClientGroup = await _managerRepository.GetGroup(queue.Id);
                    }
                    var searchResult = await GetAvaibleTablesInRestorant(queue.ClientGroup.RestorantId, queue.ClientGroup.Clients.Count());

                    if (searchResult.Tables.Any())
                    {
                        var table = searchResult.Tables.First();
                        var requestDTO = new TableRequestDTO()
                        {
                            ClientGroupId = queue.ClientGroupId,
                            WhenGroupSetAtTableDateTime = DateTime.UtcNow,
                            PlacesToTakeCount = queue.ClientGroup.Clients.Count(),
                            RequestDateTime = DateTime.UtcNow,
                            RequestTableStatus = RequestTableStatus.GroupIsAtTable,
                            TableId = table.Id
                        };

                        var request = _mapper.Map<TableRequest>(requestDTO);
                        var savedRequest = await _managerRepository.AddTableRequest(request);
                        await _managerRepository.MarkQueueAsProccessed(queue.Id);
                    }
                }
                queues = await _managerRepository.GetToProccessQueues();
            }
        }

        public async Task<EndVisitResult> EndClientsVisiting(long groupId)
        {
            var group = await _managerRepository.GetGroup(groupId);
            if (group == null)
            {
                return EndVisitResult.NotFoundGroup(groupId);
            }
            await _managerRepository.EndClientsVisiting(groupId);
            return EndVisitResult.SuccessfullyEnded(groupId);
        }

        public async Task<RestorantDTO> AddRestorant(RestorantDTO restorant)
        {
            var rest = _mapper.Map<Restorant>(restorant);
            var saved = await _managerRepository.AddRestorant(rest);
            var result = _mapper.Map<RestorantDTO>(saved);
            return result;
        }

        public async Task<RequestTableStatus> GetClientGroupLastTableStatus(long groupId)
        {
            return await _managerRepository.GetClientGroupLastTableStatus(groupId);
        }
    }
}
