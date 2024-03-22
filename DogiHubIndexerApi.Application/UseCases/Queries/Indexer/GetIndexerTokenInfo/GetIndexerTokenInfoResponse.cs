namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokenInfo;

public class GetIndexerTokenInfoResponse
{
    public required string Tick { get; set; }
    public decimal? Supply { get; set; }
    public decimal? Limit { get; set; }
    public decimal Minted { get; set; }
    public required string TransactionHash { get; set; }
    public required DateTimeOffset Date { get; set; }
    public int Decimals { get; set; }
}