﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RestManager.API.CustomExtensions;
using RestManager.DataAccess.EFCore;
using RestManager.Services.Extensions;

namespace RestManager.xTests.Base
{
    public abstract class BaseIntegrationTest
    {
        protected WebApplicationFactory<Program> GetTestApplication()
        {
            var webFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);
                    var uuId = Guid.NewGuid().ToString();
                    var dbContextString = $"Server=.;Database=restManagerDb-{uuId};Trusted_Connection=True;Integrated security=true;TrustServerCertificate=true";
                    services.AddDbContext<DataContext>(o =>
                    {
                        o.UseSqlServer(dbContextString);
                    });
                    services.AddRepositories();
                    services.AddAutoMapperProfiles();
                    services.AddService();
                    var provider = services.BuildServiceProvider();
                    var context = provider.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                });
            });
            return webFactory;
        }

        protected void DeleteDatabase(WebApplicationFactory<Program> factory)
        {
            using (var scope = factory.Services.CreateAsyncScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cxt = scopedServices.GetRequiredService<DataContext>();
                cxt.Database.EnsureDeleted();

            }
        }
    }
}
