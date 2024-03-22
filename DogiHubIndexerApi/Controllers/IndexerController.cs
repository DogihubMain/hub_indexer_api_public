using Asp.Versioning;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerLastIndexedBlockHeight;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenBalanceByWalletAddressAndTick;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;
using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogiHubIndexerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/drc20")]
    public class IndexerController(ILogger<IndexerController> logger, ISender sender) : ControllerBase
    {
        private readonly ILogger<IndexerController> _logger = logger;

        [HttpGet("transaction/{transaction-id}")]
        public async Task<IActionResult> GetTransaction(
            [FromRoute(Name = "transaction-id")] string transactionId,
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerTransactionQuery() { TransactionId = transactionId };
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
        
        [HttpGet("transaction/block/{block-height}")]
        public async Task<IActionResult> GetTransactionByBlock(
            [FromRoute(Name = "block-height")] int blockHeight,
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerTransactionByBlockQuery() { BlockHeight = blockHeight };
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
        
        [HttpGet("balance/{wallet-address}/tokens")]
        public async Task<IActionResult> GetTokensBalanceByWalletAddress(
            [FromRoute(Name = "wallet-address")] string walletAddress,
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerTokensBalanceByWalletAddressQuery() { WalletAddress = walletAddress };
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
        
        [HttpGet("balance/{wallet-address}/tokens/{tick}")]
        public async Task<IActionResult> GetTokenBalanceByWalletAddressAndTick(
            [FromRoute(Name = "wallet-address")] string walletAddress,
            [FromRoute(Name = "tick")] string tick,
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerTokenBalanceByWalletAddressAndTickQuery()
            {
                WalletAddress = walletAddress,
                Tick = tick.ToLowerInvariant()
            };
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpGet("block/last-indexed-height")]
        public async Task<IActionResult> GetLastIndexedBlockHeight(
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerLastIndexedBlockHeightQuery();
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }

        [HttpGet("token/{tick}/info")]
        public async Task<IActionResult> GetTokenInfo(
            [FromRoute(Name = "tick")] string tick,
            CancellationToken cancellationToken)
        {
            var query = new GetIndexerTokenInfoQuery() { Tick = tick.ToLowerInvariant() };
            var result = await sender.Send(query, cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
