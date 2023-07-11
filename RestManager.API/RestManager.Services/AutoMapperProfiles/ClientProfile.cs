using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO;

namespace RestManager.Services.AutoMapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
        }
    }
}
