using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestManager.DataAccess.EFCore;
using RestManager.DataAccess.Repositories;
using RestManager.DataAccess.Repositories.Interfaces;
using RestManager.Services.Interfaces;
using RestManager.Services.Queues;
using RestManager.Services.Services;

namespace RestManager.API.CustomExtensions
{
    public static class ContainerExtension
    {
        public static void AddDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
            {

                options.UseSqlServer(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRestManagerRepository, RestManagerRepository>();
        }

        public static void AddService(this IServiceCollection services)
        {
            services.AddTransient<IRestManager, Services.Services.RestManager>();
            services.AddSingleton<CheckForFreeTableQueue>();
            services.AddHostedService<CheckForFreeTableHostedService>();
        }
    }
}
