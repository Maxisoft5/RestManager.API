using Microsoft.Extensions.DependencyInjection;
using RestManager.Services.Interfaces;
using RestManager.Services.ModelDTO;
using RestManager.xTests.Base;
using Shouldly;

namespace RestManager.xTests
{
    public class RestorantTests : BaseIntegrationTest
    {

        [Fact]
        public async Task AddClientsToRestorantTest()
        {
            // Arrange
            var webFactory = GetTestApplication();
            var tables = new List<TableDTO>()
            {
                new()
                {
                    Number = 1,
                    TotalPlaces = 1,
                },
                new()
                {
                    Number = 2,
                    TotalPlaces = 2,
                },
                new()
                {
                    Number = 3,
                    TotalPlaces = 3,
                },
                new()
                {
                    Number = 4,
                    TotalPlaces = 4,
                },
            };
            var restorant = new RestorantDTO()
            {
                Address = "testAddress1",
                City = "testCity1",
                Country = "testCountry1",
                Name = "Name1",
                Tables = tables
            };
            var clients1 = new List<ClientDTO>()
            {
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName",
                    IsGroupRegistrator = true,
                    LastName = "testLastName",
                    PhoneNumber = "12345678901",
                }
            };
            var clientGroup1 = new ClientGroupDTO()
            {
                Clients = clients1
            };

            var clients2 = new List<ClientDTO>()
            {
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName2",
                    IsGroupRegistrator = true,
                    LastName = "testLastName2",
                    PhoneNumber = "12345678902",
                },
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName22",
                    IsGroupRegistrator = false,
                    LastName = "testLastName22",
                    PhoneNumber = "12345678902",
                }
            };

            var clientGroup2 = new ClientGroupDTO()
            {
                Clients = clients2
            };

            var clients4 = new List<ClientDTO>()
            {
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName4",
                    IsGroupRegistrator = true,
                    LastName = "testLastName4",
                    PhoneNumber = "12345678904",
                },
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName44",
                    IsGroupRegistrator = false,
                    LastName = "testLastName44",
                    PhoneNumber = "12345678904",
                }
            };
            var clientGroup4 = new ClientGroupDTO()
            {
                Clients = clients4
            };

            var clients5 = new List<ClientDTO>()
            {
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName4",
                    IsGroupRegistrator = true,
                    LastName = "testLastName4",
                    PhoneNumber = "12345678904",
                },
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName44",
                    IsGroupRegistrator = false,
                    LastName = "testLastName44",
                    PhoneNumber = "12345678904",
                }
            };
            var clientGroup5 = new ClientGroupDTO()
            {
                Clients = clients5
            };

            var clients6 = new List<ClientDTO>()
            {
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName4",
                    IsGroupRegistrator = true,
                    LastName = "testLastName4",
                    PhoneNumber = "12345678904",
                },
                new()
                {
                    City = restorant.City,
                    Email = "test@mail.com",
                    FirstName = "testName44",
                    IsGroupRegistrator = false,
                    LastName = "testLastName44",
                    PhoneNumber = "12345678904",
                }
            };
            var clientGroup6 = new ClientGroupDTO()
            {
                Clients = clients6
            };


            // Act
            using (var scope = webFactory.Services.CreateAsyncScope())
            {
                var scopedServices = scope.ServiceProvider;
                var restManager = scopedServices.GetRequiredService<IRestManager>();
                restorant = await restManager.AddRestorant(restorant);
                clientGroup1.RestorantId = restorant.Id;
                clientGroup2.RestorantId = restorant.Id;
                clientGroup4.RestorantId = restorant.Id;
                clientGroup5.RestorantId = restorant.Id;
                clientGroup6.RestorantId = restorant.Id;

                var freeTables = await restManager.GetAvaibleTablesInRestorant(restorant.Id, clientGroup1.Clients.Count());
                freeTables.Tables.FirstOrDefault(x => x.TotalPlaces == clientGroup1.Clients.Count()).ShouldNotBeNull();
                var res1 = await restManager.SetClientsToTable(clientGroup1, restorant.Id, freeTables.Tables.First().Id);
                res1.IsError.ShouldBeFalse();

                freeTables = await restManager.GetAvaibleTablesInRestorant(restorant.Id, clientGroup2.Clients.Count());
                freeTables.Tables.FirstOrDefault(x => x.TotalPlaces == clientGroup2.Clients.Count()).ShouldNotBeNull();
                var res2 = await restManager.SetClientsToTable(clientGroup2, restorant.Id, freeTables.Tables.First().Id);
                res2.IsError.ShouldBeFalse();

                freeTables = await restManager.GetAvaibleTablesInRestorant(restorant.Id, clientGroup4.Clients.Count());
                freeTables.Tables.FirstOrDefault(x => x.TotalPlaces >= clientGroup4.Clients.Count()).ShouldNotBeNull();
                var res4 = await restManager.SetClientsToTable(clientGroup4, restorant.Id, freeTables.Tables.First().Id);
                res4.IsError.ShouldBeFalse();

                freeTables = await restManager.GetAvaibleTablesInRestorant(restorant.Id, clientGroup5.Clients.Count());
                freeTables.Tables.FirstOrDefault(x => x.TotalPlaces >= clientGroup5.Clients.Count()).ShouldNotBeNull();
                var res5 = await restManager.SetClientsToTable(clientGroup5, restorant.Id, freeTables.Tables.First().Id);
                res5.IsError.ShouldBeFalse();


                freeTables = await restManager.GetAvaibleTablesInRestorant(restorant.Id, clientGroup6.Clients.Count());
                freeTables.Tables.FirstOrDefault(x => x.TotalPlaces >= clientGroup6.Clients.Count()).ShouldNotBeNull();
                var res6 = await restManager.SetClientsToTable(clientGroup6, restorant.Id, freeTables.Tables.First().Id);
                res5.IsError.ShouldBeFalse();


            }

        }
    }
}