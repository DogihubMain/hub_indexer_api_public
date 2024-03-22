
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerLastIndexedBlockHeight;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;

namespace DogiHubIndexerApi.Application.Services
{
    public interface IIndexerService
    {
        Task<GetIndexerLastIndexedBlockHeightResponse> GetDrc20LastIndexedBlockHeightAsync(CancellationToken cancellationToken);
        Task<GetIndexerTokensBalanceByWalletAddressResponse> GetDrc20TokensBalanceByWalletAddressAsync(string walletAddress, CancellationToken cancellationToken);
        Task<GetIndexerTokenBalanceByWalletAddressResponse?> GetDrc20TokenBalanceByWalletAddressAndTickAsync(string walletAddress, string tick, CancellationToken cancellationToken);
        Task<GetIndexerTransactionResponse?> GetDrc20TransactionAsync(string transactionId, CancellationToken cancellationToken);
        Task<GetIndexerTransactionByBlockResponse> GetIndexerTransactionByBlockAsync(int blockHeight, CancellationToken cancellationToken);
        Task<GetIndexerTokenInfoResponse?> GetDrc20TokenInfoAsync(string tick, CancellationToken cancellationToken);
    }
}
