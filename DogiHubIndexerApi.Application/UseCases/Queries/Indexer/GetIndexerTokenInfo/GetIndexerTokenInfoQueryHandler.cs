using DogiHubIndexerApi.Application.Services;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo
{
    public class GetIndexerTokenInfoQueryHandler(IDogiHubIndexerService indexerService)
        : IRequestHandler<GetIndexerTokenInfoQuery, GetIndexerTokenInfoResponse?>
    {
        public async Task<GetIndexerTokenInfoResponse?> Handle(GetIndexerTokenInfoQuery request, CancellationToken cancellationToken)
        {
            return await indexerService.GetDrc20TokenInfoAsync(request.Tick, cancellationToken);
        }
    }
}
