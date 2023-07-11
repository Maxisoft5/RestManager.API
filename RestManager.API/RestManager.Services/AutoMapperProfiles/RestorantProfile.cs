using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO;

namespace RestManager.Services.AutoMapperProfiles
{
    public class RestorantProfile : Profile
    {
        public RestorantProfile()
        {
            CreateMap<Restorant, RestorantDTO>()
                .ReverseMap();
        }
    }
}
