using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO.Abstract;

namespace RestManager.Services.ModelDTO
{
    public class TableDTO : DbDTOModel
    {
        public int Number { get; set; }
        public int TotalPlaces { get; set; }
        public long RestorantId { get; set; }
        public RestorantDTO Restorant { get; set; }
        public IEnumerable<TableRequestDTO> TableRequests { get; set; }
    }
}
