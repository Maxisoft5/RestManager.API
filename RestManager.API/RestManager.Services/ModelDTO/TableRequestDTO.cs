using RestManager.DataAccess.Models.Enums;
using RestManager.Services.ModelDTO.Abstract;

namespace RestManager.Services.ModelDTO
{
    public class TableRequestDTO : DbDTOModel
    {
        public long ClientGroupId { get; set; }
        public ClientGroupDTO ClientGroup { get; set; }
        public long TableId { get; set; }
        public TableDTO Table { get; set; }
        public DateTime RequestDateTime { get; set; }
        public DateTime? WhenGroupSetAtTableDateTime { get; set; }
        public DateTime? WhenGroupGotUpFromTableDateTime { get; set; }
        public RequestTableStatus RequestTableStatus { get; set; }
        public int PlacesToTakeCount { get; set; }
    }
}
