using DogiHubIndexerApi.Application.Services;
using MediatR;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;

public class GetIndexerTransactionByBlockHandler(IDogiHubIndexerService indexerService)
    : IRequestHandler<GetIndexerTransactionByBlockQuery, GetIndexerTransactionByBlockResponse>
{
    private readonly IDogiHubIndexerService _indexerService = indexerService;

    public async Task<GetIndexerTransactionByBlockResponse> Handle(GetIndexerTransactionByBlockQuery request, CancellationToken cancellationToken)
    {
        return await _indexerService.GetIndexerTransactionByBlockAsync(request.BlockHeight, cancellationToken);
    }
}