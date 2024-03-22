namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTokensBalanceByWalletAddress;

public class GetIndexerTokensBalanceByWalletAddressResponse
{
    public required List<GetIndexerTokenBalanceByWalletAddressResponse> Items { get; set; }
    public required int Total { get; set; }
}

public class GetIndexerTokenBalanceByWalletAddressResponse
{
    public string Tick { get; set; }
    public double? Available { get; set; }
    public double? Inscribed { get; set; }
    public double? Total { get; set; }
}