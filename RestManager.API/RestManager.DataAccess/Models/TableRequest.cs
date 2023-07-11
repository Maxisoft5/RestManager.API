using RestManager.DataAccess.Models.Abstract;
using RestManager.DataAccess.Models.Enums;

namespace RestManager.DataAccess.Models
{
    public class TableRequest : DbModel
    {
        public long ClientGroupId { get; set; }
        public ClientGroup ClientGroup { get; set; }
        public long TableId { get; set; }
        public Table Table { get; set; }
        public DateTime RequestDateTime { get; set; }
        public DateTime? WhenGroupSetAtTableDateTime { get; set; }
        public DateTime? WhenGroupGotUpFromTableDateTime { get; set; }
        public RequestTableStatus RequestTableStatus { get; set; }
        public int PlacesToTakeCount { get; set; }
    }
}
