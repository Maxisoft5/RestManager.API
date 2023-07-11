using Microsoft.EntityFrameworkCore;
using RestManager.DataAccess.EFCore;
using RestManager.DataAccess.Models;
using RestManager.DataAccess.Models.Enums;
using RestManager.DataAccess.Repositories.Interfaces;

namespace RestManager.DataAccess.Repositories
{
    public class RestManagerRepository : IRestManagerRepository
    {
        private readonly DataContext _dataContext;
        public RestManagerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ClientGroup> AddClientGroup(ClientGroup clientGroup)
        {
            await _dataContext.ClientGroups.AddAsync(clientGroup);
            await _dataContext.SaveChangesAsync();
            return clientGroup;
        }

        public async Task<IEnumerable<Client>> AddClients(IEnumerable<Client> clients)
        {
            await _dataContext.Clients.AddRangeAsync(clients);
            await _dataContext.SaveChangesAsync();
            return clients;
        }

        public async Task<QueueForTable> AddQueue(QueueForTable queueForTable)
        {
            await _dataContext.QueuesForTable.AddAsync(queueForTable);
            await _dataContext.SaveChangesAsync();
            return queueForTable;
        }

        public async Task<Restorant> AddRestorant(Restorant restorant)
        {
            await _dataContext.Restorants.AddAsync(restorant);
            await _dataContext.SaveChangesAsync();
            return restorant;
        }

        public async Task<TableRequest> AddTableRequest(TableRequest tableRequest)
        {
            await _dataContext.TableRequests.AddAsync(tableRequest);
            await _dataContext.SaveChangesAsync();
            return tableRequest;
        }

        public async Task<TableRequest> EndClientsVisiting(long groupId)
        {
            var request = await _dataContext.TableRequests
                .Include(x => x.ClientGroup).FirstOrDefaultAsync(x => x.ClientGroupId == groupId
                    && x.RequestTableStatus == Models.Enums.RequestTableStatus.GroupIsAtTable);

            request.RequestTableStatus = Models.Enums.RequestTableStatus.GroupHasLeftFromTable;
            _dataContext.TableRequests.Update(request);
            await _dataContext.SaveChangesAsync();

            return request;
        }

        public async Task<RequestTableStatus> GetClientGroupLastTableStatus(long groupId)
        {
            var request = await _dataContext.TableRequests.OrderByDescending(x => x.RequestDateTime)
                .FirstOrDefaultAsync(x => x.ClientGroupId == groupId);

            return request.RequestTableStatus;
        }

        public async Task<ClientGroup> GetGroup(long groupId)
        {
            return await _dataContext.ClientGroups.Include(x => x.Clients)
                .FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<Restorant> GetRestorant(long restorantId)
        {
            var restorant = await _dataContext.Restorants
                    .Include(x => x.Tables).ThenInclude(x => x.TableRequests)
                    .FirstOrDefaultAsync(x => x.Id == restorantId);

            return restorant;
        }

        public async Task<IEnumerable<QueueForTable>> GetToProccessQueues()
        {
            return await _dataContext.QueuesForTable.Include(x => x.ClientGroup).ThenInclude(x => x.Restorant)
                .Where(x => x.QueueForTableStatus == Models.Enums.QueueForTableStatus.ToProcessed)
                .ToListAsync();
        }

        public async Task<QueueForTable> MarkQueueAsProccessed(long queueId)
        {
            var queue = _dataContext.Set<QueueForTable>()
               .Local.FirstOrDefault(entry => entry.Id.Equals(queueId));

            queue.QueueForTableStatus = Models.Enums.QueueForTableStatus.Processed;
            if (queue != null)
            {
                _dataContext.Entry(queue).State = EntityState.Modified;
            } 
            else
            {
                _dataContext.QueuesForTable.Update(queue);
            }

            await _dataContext.SaveChangesAsync();
            return queue;
        }
    }
}
