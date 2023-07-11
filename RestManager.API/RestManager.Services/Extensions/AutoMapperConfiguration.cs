using Microsoft.Extensions.DependencyInjection;
using RestManager.Services.AutoMapperProfiles;

namespace RestManager.Services.Extensions
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ClientGroupProfile));
            services.AddAutoMapper(typeof(ClientProfile));
            services.AddAutoMapper(typeof(RestorantProfile));
            services.AddAutoMapper(typeof(TableProfile));
            services.AddAutoMapper(typeof(TableRequestProfile));
            services.AddAutoMapper(typeof(QueueForTableProfile));
        }
    }
}
