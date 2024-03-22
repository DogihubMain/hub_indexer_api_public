using System.Text.Json.Serialization;
using DogiHubIndexerApi.Application.Helpers;

namespace DogiHubIndexerApi.Application.Entities;

public class InscriptionEntity
{
    [JsonIgnore] private bool IsToken => TokenContent != null;

    [JsonIgnore] private bool IsDogemap => DogemapContent != null;

    [JsonIgnore] private bool IsDns => DnsContent != null;

    [JsonIgnore] private bool IsNft => NftContent != null;

    [JsonIgnore] public string Id { get; set; }
    [JsonPropertyName("t")] public TokenInscriptionContentContract? TokenContent { get; init; }
    [JsonPropertyName("m")] public DogemapInscriptionContentContract? DogemapContent { get; init; }
    [JsonPropertyName("d")] public DnsInscriptionContentContract? DnsContent { get; init; }
    [JsonPropertyName("n")] public NftInscriptionContentContract? NftContent { get; init; }
    [JsonIgnore] public string GenesisTxId { get; set; }
    [JsonPropertyName("c")] public required string ContentType { get; init; }
    [JsonPropertyName("s")] public required DateTimeOffset Timestamp { get; init; }

    [JsonIgnore()]
    public InscriptionTypeEnum Type
    {
        get
        {
            if (IsToken) return InscriptionTypeEnum.Token;
            if (IsDogemap) return InscriptionTypeEnum.Dogemap;
            if (IsDns) return InscriptionTypeEnum.Dns;
            if (IsNft) return InscriptionTypeEnum.Nft;
            throw new ArgumentException("Invalid inscription type", nameof(InscriptionTypeEnum));
        }
    }

    [JsonIgnore]
    public string Name
    {
        get
        {
            if (IsToken) return TokenContent!.tick;
            if (IsDogemap) return DogemapContent!.Name;
            if (IsDns) return DnsContent!.name;
            if (IsNft) return Id.ToString();
            throw new ArgumentException("Invalid name", nameof(InscriptionTypeEnum));
        }
    }

    public static InscriptionEntity? Build(string? json, string inscriptionId)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;

        var readModel = JsonHelper.Deserialize<InscriptionEntity>(json!);

        if (readModel != null)
        {
            readModel.Id = inscriptionId;
            readModel.GenesisTxId = inscriptionId.Replace("i0", string.Empty);
        }

        return readModel;
    }
}

public enum InscriptionTypeEnum
{
    Token,
    Dogemap,
    Dns,
    Nft
}

public class TokenInscriptionContentContract
{
    private string _tick;

    [JsonIgnore] public string p { get; set; }
    public required string op { get; set; }

    public required string tick
    {
        get => _tick;
        set => _tick = value.ToLowerInvariant();
    }

    public string? amt { get; set; }
    public string? max { get; set; }
    public string? lim { get; set; }
    public string? dec { get; set; }
}

public class DogemapInscriptionContentContract
{
    [JsonPropertyName("n")] public required string Name { get; set; }
    [JsonIgnore] public int Number => int.Parse(Name.Split('.')[0]);
}

public class DnsInscriptionContentContract
{
    public required string p { get; set; }
    public required string op { get; set; }
    public required string name { get; set; }
}

public class NftInscriptionContentContract
{
    [JsonPropertyName("ids")] public required List<string> TxIds { get; set; }

    //allow us if all transactions ids has already been get
    [JsonPropertyName("c")] public required bool IsComplete { get; set; } = false;
}