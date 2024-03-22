using DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;

namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransactionByBlock;

public class GetIndexerTransactionByBlockResponse
{
    public required List<GetIndexerTransactionResponse> Items { get; set; }
    public required int Total { get; set; }

}