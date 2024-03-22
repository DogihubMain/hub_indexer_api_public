namespace DogiHubIndexerApi.Application.UseCases.Queries.Indexer.GetIndexerTransaction;

public class GetIndexerTransactionResponse
{
    public required string TransactionId { get; set; }
    public required DateTime Date { get; set; }
    public required string InscriptionId { get; set; }
    public required string Type { get; set; }
    public string? TokenTick { get; set; }
    public string? Amount { get; set; }
    public required int BlockHeight { get; set; }
    public string? Address { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
}