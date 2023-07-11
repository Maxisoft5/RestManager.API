using RestManager.DataAccess.Models.Enums;
using RestManager.Services.ModelDTO.Abstract;

namespace RestManager.Services.ModelDTO
{
    public class QueueForTableDTO : DbDTOModel
    {
        public long ClientGroupId { get; set; }
        public ClientGroupDTO ClientGroup { get; set; }
        public QueueForTableStatus QueueForTableStatus { get; set; }

    }
}
