using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO;

namespace RestManager.Services.AutoMapperProfiles
{
    public class ClientGroupProfile : Profile
    {
        public ClientGroupProfile()
        {
            CreateMap<ClientGroup, ClientGroupDTO>()
                .ReverseMap();
        }
    }
}
