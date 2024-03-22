using DogiHubIndexerApi.Application.Services;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress
{
    public class GetIndexerTokensBalanceByWalletAddressQueryHandler(IDogiHubIndexerService indexerService)
        : IRequestHandler<GetIndexerTokensBalanceByWalletAddressQuery, GetIndexerTokensBalanceByWalletAddressResponse>
    {
        public async Task<GetIndexerTokensBalanceByWalletAddressResponse> Handle(GetIndexerTokensBalanceByWalletAddressQuery request, CancellationToken cancellationToken)
        {
            return await indexerService.GetDrc20TokensBalanceByWalletAddressAsync(request.WalletAddress, cancellationToken);
        }
    }
}
