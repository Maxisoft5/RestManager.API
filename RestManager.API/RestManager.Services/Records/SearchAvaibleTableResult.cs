using RestManager.Services.ModelDTO;

namespace RestManager.Services.Records
{
    public record SearchAvaibleTableResult
    {
        public long RestorantId { get; init; }
        public string Message { get; init; }
        public bool IsError { get; init; }
        public IEnumerable<TableDTO> Tables { get; init; }
        private SearchAvaibleTableResult(long restorantId, string message,
            IEnumerable<TableDTO> tables, bool isError) 
        { 
            Message = message;
            RestorantId = restorantId;
            Tables = tables;
            IsError = isError;
        }

        public static SearchAvaibleTableResult NotFoundRestorant(long restorantId) => 
            new(restorantId, $"Restorant with RestorantId {restorantId} was not found", Enumerable.Empty<TableDTO>(), true);

        public static SearchAvaibleTableResult NotFoundTables(long restorantId) =>
           new(restorantId, $"Free tables for RestorantId {restorantId} was not found", Enumerable.Empty<TableDTO>(), false);

        public static SearchAvaibleTableResult SuccessfullyFound(long restorantId, IEnumerable<TableDTO> tables) =>
            new(restorantId, $"Found {tables.Count()} tables", tables, false);


    }
}
