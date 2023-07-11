namespace RestManager.Services.Records
{
    public record QueueForNextTableResult
    {
        public long QueueId { get; init; }
        public long ClientGroupId { get; init; }
        public long RestorantId { get; init; }
        public bool IsError { get; init; }
        public string Message { get; init; }
        private QueueForNextTableResult(long restorantId, string message, long clientGroupId, long queueId, bool isError)
        {
            Message = message;
            RestorantId = restorantId;
            ClientGroupId = clientGroupId;
            QueueId = queueId;
            IsError = isError;
        }

        public static QueueForNextTableResult NotFoundRestorant(long restorantId) =>
            new(restorantId, $"Restorant with RestorantId {restorantId} was not found and group wasn't been registered", 0, 0, true);

        public static QueueForNextTableResult SuccessfullyAdded(long restorantId, long clientGroupId, long queueId) =>
            new(restorantId, $"Qeueu for RestorantId {restorantId} and clientGroupId {clientGroupId} was added", clientGroupId,
                queueId, false);

    }
}
