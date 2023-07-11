using RestManager.Services.ModelDTO.Abstract;

namespace RestManager.Services.ModelDTO
{
    public class ClientDTO : DbDTOModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string City { get; set; }
        public bool IsGroupRegistrator { get; set; }
        public long GroupId { get; set; }
    }
}
