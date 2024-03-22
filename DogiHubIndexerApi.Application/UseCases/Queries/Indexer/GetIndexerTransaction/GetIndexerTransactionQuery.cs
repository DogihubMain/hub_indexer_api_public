using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction
{
    public record GetIndexerTransactionQuery : IRequest<GetIndexerTransactionResponse>
    {
        public required string TransactionId { get; init; }
    }
}
