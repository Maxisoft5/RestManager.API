using System.Text.RegularExpressions;

namespace RestManager.Services.Records
{
    public record EndVisitResult
    {
        public EndVisitResult(bool isError, string message, long groupId)
        {
            IsError = isError;
            Message = message;
            GroupId = groupId;
        }

        public bool IsError { get; init; }
        public string Message { get; init; }
        public long GroupId { get; init; }

        public static EndVisitResult SuccessfullyEnded(long groupId) =>
            new(false, $"GroupId {groupId} was ended visiting", groupId);

        public static EndVisitResult NotFoundGroup(long groupId) =>
            new(false, $"GroupId {groupId} was not found", groupId);

    }
}
