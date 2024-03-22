using DogiHubIndexerApi.Application.Services;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenBalanceByWalletAddressAndTick;

public class GetIndexerTokenBalanceByWalletAddressAndTickQueryHandler(IDogiHubIndexerService indexerService)
    : IRequestHandler<GetIndexerTokenBalanceByWalletAddressAndTickQuery, GetIndexerTokenBalanceByWalletAddressResponse?>
{
    public async Task<GetIndexerTokenBalanceByWalletAddressResponse?> Handle(GetIndexerTokenBalanceByWalletAddressAndTickQuery request, CancellationToken cancellationToken)
    {
        return await indexerService.GetDrc20TokenBalanceByWalletAddressAndTickAsync(request.WalletAddress, request.Tick, cancellationToken);
    }
}