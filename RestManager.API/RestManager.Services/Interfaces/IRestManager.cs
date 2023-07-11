using RestManager.DataAccess.Models.Enums;
using RestManager.Services.ModelDTO;
using RestManager.Services.Records;

namespace RestManager.Services.Interfaces
{
    public interface IRestManager
    {
        public Task<ClientGroupDTO> AddClientGroup(ClientGroupDTO group, long restorantId);
        public Task<RestorantDTO> AddRestorant(RestorantDTO restorant);
        public Task<RequestTableStatus> GetClientGroupLastTableStatus(long groupId);
        public Task<SearchAvaibleTableResult> GetAvaibleTablesInRestorant(long restorantId, int clientsCount);
        public Task<SetClientsResult> SetClientsToTable(ClientGroupDTO clientGroup, long restorantId, long tableId);
        public Task<QueueForNextTableResult> QueueForNextAvaibleTable(ClientGroupDTO clientGroup, long restorantId);
        public Task<EndVisitResult> EndClientsVisiting(long groupId);
        public Task ProccessQueues();
    }
}
