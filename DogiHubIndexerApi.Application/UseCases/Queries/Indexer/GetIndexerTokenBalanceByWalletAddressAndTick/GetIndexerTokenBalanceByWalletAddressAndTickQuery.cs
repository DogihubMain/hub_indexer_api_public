using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenBalanceByWalletAddressAndTick;

public record GetIndexerTokenBalanceByWalletAddressAndTickQuery : IRequest<GetIndexerTokenBalanceByWalletAddressResponse>
{
    public required string WalletAddress { get; init; }
    public required string Tick { get; init; }
}