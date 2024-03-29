﻿using RestManager.Services.ModelDTO;

namespace RestManager.Services.Records
{
    public record SetClientsResult
    {
        public SetClientsResult(long restorantId, string message, bool isError, ClientGroupDTO? clientGroup,
            long tableId)
        {
            RestorantId = restorantId;
            Message = message;
            IsError = isError;
            ClientGroup = clientGroup;
            Table = tableId;
        }

        public long RestorantId { get; init; }
        public string Message { get; init; }
        public bool IsError { get; init; }
        public ClientGroupDTO? ClientGroup { get; init; }
        public long Table { get; init; }

        public static SetClientsResult NotFoundRestorant(long restorantId, ClientGroupDTO clientGroup, long tableId) =>
            new(restorantId, $"Restorant with RestorantId {restorantId} was not found", true, clientGroup, tableId);
        public static SetClientsResult GroupWasSentToQueue(long restorantId, ClientGroupDTO clientGroup, long tableId) =>
            new(restorantId, $"Restorant with RestorantId {restorantId} haven't free table places, group with id {clientGroup.Id} was sent to queue", true, clientGroup, tableId);
        public static SetClientsResult UnExcpectedErrorWhenTryToSetToQueue(long restorantId, ClientGroupDTO clientGroup, long tableId, string error) =>
           new(restorantId, $"Restorant with RestorantId {restorantId} haven't free table places, group with id {clientGroup.Id} was sent to queue but error was happend ${error}", true, clientGroup, tableId);
        public static SetClientsResult SuccessfullyAdded(long restorantId, ClientGroupDTO clientGroup, long tableId) =>
            new(restorantId, $"Added {clientGroup.Clients.Count()} Clients to table id {tableId}", false, clientGroup, tableId);
    }
}
