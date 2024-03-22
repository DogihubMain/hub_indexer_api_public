using DogiHubIndexerApi.Application.Services;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerLastIndexedBlockHeight
{
    public class GetIndexerLastIndexedBlockHeightQueryHandler(IDogiHubIndexerService indexerService)
        : IRequestHandler<GetIndexerLastIndexedBlockHeightQuery, GetIndexerLastIndexedBlockHeightResponse>
    {
        public async Task<GetIndexerLastIndexedBlockHeightResponse> Handle(GetIndexerLastIndexedBlockHeightQuery request, CancellationToken cancellationToken)
        {
            return await indexerService.GetDrc20LastIndexedBlockHeightAsync(cancellationToken);
        }
    }
}
