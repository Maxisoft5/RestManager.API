using AutoMapper;
using RestManager.DataAccess.Models;
using RestManager.Services.ModelDTO;

namespace RestManager.Services.AutoMapperProfiles
{
    public class QueueForTableProfile : Profile
    {
        public QueueForTableProfile()
        {
            CreateMap<QueueForTable, QueueForTableDTO>()
                .ReverseMap();
        }
    }
}
