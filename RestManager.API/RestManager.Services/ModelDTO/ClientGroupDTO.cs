using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO.Abstract;

namespace RestManager.Services.ModelDTO
{
    public class ClientGroupDTO : DbDTOModel
    {
        public long RestorantId { get; set; }
        public RestorantDTO? Restorant { get; set; }
        public long? QueueForTableId { get; set; }
        public QueueForTableDTO? QueueForTable { get; set; }
        public IEnumerable<ClientDTO> Clients { get; set; }
        public IEnumerable<TableRequestDTO>? TableRequests { get; set; }
    }
}
