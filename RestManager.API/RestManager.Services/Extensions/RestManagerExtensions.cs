using RestManager.DataAccess.Models;
using RestManager.DataAccess.Models.Enums;

namespace RestManager.Services.Extensions
{
    public static class RestManagerExtensions
    {
        public static IEnumerable<Table> FirstNotFullyFreeButHaveSpace(this IEnumerable<Table> tables,
                int clientsCount)
        {
            var available = tables.Where(x => x.TotalPlaces >= clientsCount &&
                (x.TotalPlaces - x.TableRequests
                .Where(x => x.RequestTableStatus == RequestTableStatus.GroupIsAtTable)
                    .Sum(x => x.PlacesToTakeCount)) <= clientsCount);

            var tableToMatchTotalPlaces = available
                .Where(x =>
                (x.TableRequests.Sum(x => x.PlacesToTakeCount) + clientsCount) == x.TotalPlaces);

            return tableToMatchTotalPlaces.Any() ? tableToMatchTotalPlaces : available;
        }

        public static IEnumerable<Table> FirstFullyFree(this IEnumerable<Table> tables,
              int clientsCount)
        {
            return tables.Where(x => x.TotalPlaces >= clientsCount &&
                (x.TableRequests.All(x => x.RequestTableStatus == RequestTableStatus.GroupHasLeftFromTable) 
                || x.TableRequests.Count() == 0  ) );
        }
    }
}
