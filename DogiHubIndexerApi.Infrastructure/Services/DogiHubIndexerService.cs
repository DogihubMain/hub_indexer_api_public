using DogiHubIndexerApi.Application.Clients;
using DogiHubIndexerApi.Application.Configuration;
using DogiHubIndexerApi.Application.Entities;
using DogiHubIndexerApi.Application.Extensions;
using DogiHubIndexerApi.Application.Services;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerLastIndexedBlockHeight;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;

namespace DogiHubIndexerApi.Infrastructure.Services
{
    public class DogiHubIndexerService(IRedisClient redisClient) : IDogiHubIndexerService
    {
        public async Task<GetIndexerLastIndexedBlockHeightResponse> GetDrc20LastIndexedBlockHeightAsync(
            CancellationToken cancellationToken)
        {
            string lastReadModelsBlockSyncKey = RedisKeys.GetLastReadModelsBlockSyncKey();

            var stringValue = await redisClient.StringGetAsync(lastReadModelsBlockSyncKey);

            if (!string.IsNullOrWhiteSpace(stringValue)
                && int.TryParse(stringValue, out int intValue))
            {
                return new GetIndexerLastIndexedBlockHeightResponse() { BlockHeight = intValue };
            }
            else
            {
                return new GetIndexerLastIndexedBlockHeightResponse() { BlockHeight = 0 };
            }
        }

        public async Task<GetIndexerTokensBalanceByWalletAddressResponse> GetDrc20TokensBalanceByWalletAddressAsync(
            string walletAddress, CancellationToken cancellationToken)
        {
            var userTokens = await GetUserBalanceTokensAsync(walletAddress);
            var filteredUserTokens = userTokens.Where(x => x.BalanceSum > 0).ToList();

            return new GetIndexerTokensBalanceByWalletAddressResponse()
            {
                Total = filteredUserTokens.Count(),
                Items = filteredUserTokens.Select(x => new GetIndexerTokenBalanceByWalletAddressResponse()
                {
                    Total = (double)x.BalanceSum,
                    Available = (double?)x.Available,
                    Inscribed = (double?)x.Transferable,
                    Tick = x.TokenTick,
                }).ToList()
            };
        }

        public async Task<GetIndexerTokenBalanceByWalletAddressResponse?>
            GetDrc20TokenBalanceByWalletAddressAndTickAsync(string walletAddress, string tick,
                CancellationToken cancellationToken)
        {
            var balanceDetailKey = RedisKeys.GetBalanceDetailKey(walletAddress, tick);
            var userBalanceTokenContract = await GetUserBalanceTokenAsync(walletAddress, balanceDetailKey);

            if (userBalanceTokenContract == null)
            {
                return null;
            }

            return new GetIndexerTokenBalanceByWalletAddressResponse()
            {
                Total = (double)userBalanceTokenContract.BalanceSum,
                Available = (double?)userBalanceTokenContract.Available,
                Inscribed = (double?)userBalanceTokenContract.Transferable,
                Tick = userBalanceTokenContract.TokenTick,
            };
        }

        public async Task<GetIndexerTransactionResponse?> GetDrc20TransactionAsync(string transactionId,
            CancellationToken cancellationToken)
        {
            var inscriptionTransferHashKey = RedisKeys.GetInscriptionTransferHashKey(transactionId);
            var json = await redisClient.StringGetAsync(inscriptionTransferHashKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var inscriptionTransfer = InscriptionTransferEntity.Build(json, transactionId);

                if (inscriptionTransfer != null
                    && (inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.DEPLOY
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.MINT
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.INSCRIBE_TRANSFER
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.PENDING_TRANSFER
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.PENDING_DNS
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.PENDING_NFT
                        || inscriptionTransfer.InscriptionTransferType == InscriptionTransferType.PENDING_DOGEMAP)
                   )
                {
                    inscriptionTransfer.Inscription = await GetDrc20InscriptionAsync(inscriptionTransfer.InscriptionId);
                    return inscriptionTransfer.ToGetIndexerTransactionResponse();
                }
            }

            return null;
        }

        public async Task<GetIndexerTransactionByBlockResponse> GetIndexerTransactionByBlockAsync(int blockHeight, CancellationToken cancellationToken)
        {
            var inscriptionTransfers = await GetInscriptionTransfersByBlockAsync(blockHeight);

            return new GetIndexerTransactionByBlockResponse()
            {
                Total = inscriptionTransfers.Count,
                Items = inscriptionTransfers.Any() 
                    ? inscriptionTransfers!.Select(inscriptionTransfer => inscriptionTransfer.ToGetIndexerTransactionResponse()).OrderBy(x => x.Date).ToList()
                    : new List<GetIndexerTransactionResponse>()
            };
        }
        
        private async Task<List<InscriptionTransferEntity>> GetInscriptionTransfersByBlockAsync(int blockNumber)
        {
            var inscriptionTransferByBlockKey = RedisKeys.GetInscriptionTransferByBlockKey(blockNumber);
            var hashKeys = await redisClient.SetMembersAsync(inscriptionTransferByBlockKey);

            var tasks = hashKeys.Select(async key =>
            {
                var result = await redisClient.StringGetAsync(key!);
                var transactionHash = key.ToString().Split(new[] { ':' }, 2)[1];
                return InscriptionTransferEntity.Build(result, transactionHash);
            });

            var results = await Task.WhenAll(tasks);

            List<InscriptionTransferEntity> inscriptionTransfersWithoutInscriptions = results.Where(result => result != null).ToList()!;

            if (inscriptionTransfersWithoutInscriptions.Any())
            {
                var enrichTasks = inscriptionTransfersWithoutInscriptions.Select(inscriptionTransfer => GetDrc20InscriptionAsync(inscriptionTransfer.InscriptionId)).ToArray();
                var enrichedTokens = await Task.WhenAll(enrichTasks);

                for (int i = 0; i < inscriptionTransfersWithoutInscriptions.Count(); i++)
                {
                    inscriptionTransfersWithoutInscriptions[i]!.Inscription = enrichedTokens[i];
                }
                return inscriptionTransfersWithoutInscriptions.Where(x => x.Inscription != null).ToList();
            }
            return inscriptionTransfersWithoutInscriptions;
        }


        private async Task<InscriptionEntity?> GetDrc20InscriptionAsync(string inscriptionId)
        {
            var inscriptionHashKey = RedisKeys.GetInscriptionKey(inscriptionId);
            var json = await redisClient.StringGetAsync(inscriptionHashKey);
            return InscriptionEntity.Build(json, inscriptionId);
        }

        private async Task<List<UserBalanceTokenEntity>> GetUserBalanceTokensAsync(string walletAddress)
        {
            var userBalanceKey = RedisKeys.GetUserBalanceKey(InscriptionTypeEntity.Token, walletAddress);

            var balanceDetailKeys = await redisClient.SortedSetRangeByRankAsync(userBalanceKey);

            var tasks = new List<Task<UserBalanceTokenEntity>>();

            foreach (var balanceDetailKey in balanceDetailKeys)
            {
                var task = GetUserBalanceTokenAsync(walletAddress, balanceDetailKey.ToString());
                tasks.Add(task!);
            }

            var userBalances = await Task.WhenAll(tasks);
            return [..userBalances];
        }

        private async Task<UserBalanceTokenEntity?> GetUserBalanceTokenAsync(string walletAddress,
            string balanceDetailKey)
        {
            var json = await redisClient.StringGetAsync(balanceDetailKey!);
            if (!string.IsNullOrWhiteSpace(json))
            {
                string tokenTick = balanceDetailKey.Split(':')[2];
                return UserBalanceTokenEntity.Build(json, tokenTick, walletAddress);
            }

            return null;
        }

        public async Task<GetIndexerTokenInfoResponse?> GetDrc20TokenInfoAsync(string tick,
            CancellationToken cancellationToken)
        {
            string tokenInfoKey = RedisKeys.GetTokenInfoKey(tick);

            var json = await redisClient.StringGetAsync(tokenInfoKey);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var token = TokenEntity.Build(json, tick);

                return new GetIndexerTokenInfoResponse()
                {
                    Date = token!.Date,
                    Tick = token.Tick,
                    TransactionHash = token.TransactionHash,
                    Limit = token.Lim,
                    Minted = token.CurrentSupply,
                    Supply = token.Max,
                    Decimals = token.Decimal ?? 18
                };
            }

            return null;
        }
    }
}