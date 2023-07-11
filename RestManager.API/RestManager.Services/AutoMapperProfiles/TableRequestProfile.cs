using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO;

namespace RestManager.Services.AutoMapperProfiles
{
    public class TableRequestProfile : Profile
    {
        public TableRequestProfile()
        {
            CreateMap<TableRequest, TableRequestDTO>().ReverseMap();
        }
    }
}
