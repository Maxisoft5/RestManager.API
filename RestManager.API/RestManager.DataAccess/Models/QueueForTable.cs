using RestManager.DataAccess.Models.Abstract;
using RestManager.DataAccess.Models.Enums;

namespace RestManager.DataAccess.Models
{
    public class QueueForTable : DbModel
    {
        public long ClientGroupId { get; set; }
        public ClientGroup ClientGroup { get; set; }
        public QueueForTableStatus QueueForTableStatus { get; set; }
    }
}
