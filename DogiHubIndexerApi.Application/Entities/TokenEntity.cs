using System.Text.Json.Serialization;
using DogiHubIndexerApi.Application.Helpers;

namespace DogiHubIndexerApi.Application.Entities
{
    public class TokenEntity
    {
        private string _tick;

        [JsonIgnore]
        public string Tick
        {
            get => _tick;
            set => _tick = value.ToLowerInvariant();
        }

        [JsonPropertyName("m")] public decimal? Max { get; set; }
        [JsonPropertyName("l")] public decimal? Lim { get; set; }
        [JsonPropertyName("s")] public decimal CurrentSupply { get; set; }
        [JsonIgnore] public string Protocol { get; set; } = "drc-20";
        [JsonPropertyName("t")] public required string TransactionHash { get; set; }
        [JsonPropertyName("d")] public DateTimeOffset Date { get; set; }
        [JsonPropertyName("e")] public int? Decimal { get; set; }
        [JsonPropertyName("b")] public required string DeployedBy { get; set; }

        public static TokenEntity? Build(string? json, string tick)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            var contract = JsonHelper.Deserialize<TokenEntity>(json!);

            if (contract != null)
            {
                contract.Tick = tick;
            }

            return contract;
        }
    }
}