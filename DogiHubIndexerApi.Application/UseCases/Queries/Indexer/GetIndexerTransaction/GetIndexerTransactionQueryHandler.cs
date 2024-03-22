using DogiHubIndexerApi.Application.Services;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction
{
    public class GetIndexerTransactionQueryHandler(IDogiHubIndexerService indexerService)
        : IRequestHandler<GetIndexerTransactionQuery, GetIndexerTransactionResponse?>
    {
        public async Task<GetIndexerTransactionResponse?> Handle(GetIndexerTransactionQuery request, CancellationToken cancellationToken)
        {
            return await indexerService.GetDrc20TransactionAsync(request.TransactionId, cancellationToken);
        }
    }
}
