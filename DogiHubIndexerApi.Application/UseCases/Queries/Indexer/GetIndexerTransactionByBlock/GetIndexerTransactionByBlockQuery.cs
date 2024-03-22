using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;

public record GetIndexerTransactionByBlockQuery : IRequest<GetIndexerTransactionByBlockResponse>
{
    public required int BlockHeight { get; init; }
}