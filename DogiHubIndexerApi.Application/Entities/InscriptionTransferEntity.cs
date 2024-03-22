using System.ComponentModel;
using System.Text.Json.Serialization;
using DogiHubIndexerApi.Application.Helpers;

namespace DogiHubIndexerApi.Application.Entities;

public record InscriptionTransferEntity
{
    [JsonPropertyName("b")] public required int BlockNumber { get; set; }
    [JsonPropertyName("p")] public int TransactionIndex { get; set; }
    [JsonIgnore] public string TransactionHash { get; set; }
    [JsonPropertyName("x")] public required uint InputIndex { get; set; }
    [JsonIgnore] public InscriptionEntity? Inscription { get; set; }
    [JsonPropertyName("i")] public required string InscriptionId { get; init; }
    [JsonPropertyName("t")] public required InscriptionTransferType InscriptionTransferType { get; set; }
    [JsonPropertyName("r")] public required string Receiver { get; set; }
    [JsonPropertyName("s")] public string? Sender { get; set; }
    [JsonPropertyName("d")] public DateTimeOffset Date { get; set; }
    [JsonPropertyName("a")] public decimal? Amount { get; set; }

    public static InscriptionTransferEntity? Build(string? json, string transactionHash)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;

        var rawData = JsonHelper.Deserialize<InscriptionTransferEntity>(json!);

        if (rawData != null)
        {
            rawData.TransactionHash = transactionHash;
        }

        return rawData;
    }
}

public enum InscriptionTransferType
{
    [Description("d")] DEPLOY,
    [Description("m")] MINT,
    [Description("it")] INSCRIBE_TRANSFER,

    //PENDING ONES
    [Description("pt")] PENDING_TRANSFER,
    [Description("pd")] PENDING_DNS,
    [Description("pm")] PENDING_DOGEMAP,
    [Description("pn")] PENDING_NFT,

    [Description("ct")] CONFIRMED_TRANSFER,
    [Description("cd")] CONFIRMED_DNS,
    [Description("cm")] CONFIRMED_DOGEMAP,
    [Description("cn")] CONFIRMED_NFT,
}