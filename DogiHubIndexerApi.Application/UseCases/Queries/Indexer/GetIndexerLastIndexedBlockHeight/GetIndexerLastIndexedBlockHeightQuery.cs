using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerLastIndexedBlockHeight
{
    public record GetIndexerLastIndexedBlockHeightQuery : IRequest<GetIndexerLastIndexedBlockHeightResponse>
    {
    }
}
