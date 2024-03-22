using System.Text.Json.Serialization;
using System.Text.Json;

namespace DogiHubIndexerApi.Application.Helpers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, Options);
        }

        public static T Deserialize<T>(string serialized)
        {
            return JsonSerializer.Deserialize<T>(serialized, Options) ?? throw new JsonException("Deserialization failed");
        }
    }
}
