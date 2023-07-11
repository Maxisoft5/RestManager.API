using RestManager.DataAccess.Models.Abstract;

namespace RestManager.DataAccess.Models
{
    public class Restorant : DbModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public IEnumerable<Table>? Tables { get; set; }
    }
}
