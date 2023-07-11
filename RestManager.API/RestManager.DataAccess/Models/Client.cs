using RestManager.DataAccess.Models.Abstract;

namespace RestManager.DataAccess.Models
{
    public class Client : DbModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string City { get; set; }
        public bool IsGroupRegistrator { get; set; }
        public long? GroupId { get; set; }
        public ClientGroup? Group { get; set; }
    }
}
