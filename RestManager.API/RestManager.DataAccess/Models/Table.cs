using RestManager.DataAccess.Models.Abstract;

namespace RestManager.DataAccess.Models
{
    public class Table : DbModel
    {
        public int Number { get; set; }
        public int TotalPlaces { get; set; }
        public long RestorantId { get; set; }
        public Restorant Restorant { get; set; }
        public IEnumerable<TableRequest>? TableRequests { get; set; }
    }
}
