using RestManager.DataAccess.Models.Abstract;

namespace RestManager.DataAccess.Models
{
    public class ClientGroup : DbModel
    {
        public long RestorantId { get; set; }
        public Restorant Restorant { get; set; }
        public long? QueueForTableId { get; set; }
        public QueueForTable? QueueForTable { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<TableRequest> TableRequests { get; set; }
    }
}
