using RestManager.DataAccess.Models;

namespace RestManager.DataAccess.Repositories.Interfaces
{
    public interface IRestManagerRepository
    {
        public Task<Restorant> GetRestorant(long restorantId);
        public Task<ClientGroup> GetGroup(long groupId);
        public Task<IEnumerable<QueueForTable>> GetToProccessQueues();
        public Task<TableRequest> AddTableRequest(TableRequest tableRequest);
        public Task<ClientGroup> AddClientGroup(ClientGroup clientGroup);
        public Task<IEnumerable<Client>> AddClients(IEnumerable<Client> clients);
        public Task<QueueForTable> AddQueue(QueueForTable queueForTable);
        public Task<Restorant> AddRestorant(Restorant restorant); 
        public Task<QueueForTable> MarkQueueAsProccessed(long queueId);
        public Task<TableRequest> EndClientsVisiting(long groupId);
    }
}
