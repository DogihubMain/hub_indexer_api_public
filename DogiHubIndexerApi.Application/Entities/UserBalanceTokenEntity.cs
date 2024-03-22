using System.Text.Json.Serialization;
using DogiHubIndexerApi.Application.Helpers;

namespace DogiHubIndexerApi.Application.Entities
{
    public record UserBalanceTokenEntity
    {
        private string _tokenTick;

        [JsonIgnore] public string Address { get; set; }

        [JsonIgnore]
        public string TokenTick
        {
            get => _tokenTick;
            set => _tokenTick = value.ToLowerInvariant();
        }

        [JsonPropertyName("a")] public decimal? Available { get; set; }
        [JsonPropertyName("t")] public decimal? Transferable { get; set; }
        [JsonPropertyName("p")] public decimal? Pending { get; set; }

        [JsonIgnore]
        public decimal BalanceSum =>
            Available.GetValueOrDefault(0) + Transferable.GetValueOrDefault(0) +
            Pending.GetValueOrDefault(0);

        public static UserBalanceTokenEntity? Build(string? json, string tick, string address)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            var readModel = JsonHelper.Deserialize<UserBalanceTokenEntity>(json!);

            if (readModel != null)
            {
                readModel.TokenTick = tick;
                readModel.Address = address;
            }

            return readModel;
        }
    }
}