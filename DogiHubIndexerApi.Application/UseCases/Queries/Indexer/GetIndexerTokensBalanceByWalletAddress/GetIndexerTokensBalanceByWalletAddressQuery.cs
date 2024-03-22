using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress
{
    public record GetIndexerTokensBalanceByWalletAddressQuery : IRequest<GetIndexerTokensBalanceByWalletAddressResponse>
    {
        public required string WalletAddress { get; init; }
    }
}
