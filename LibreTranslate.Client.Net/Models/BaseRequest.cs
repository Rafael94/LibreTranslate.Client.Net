using System.Text.Json.Serialization;

namespace LibreTranslate.Client.Net.Models;

public class BaseRequest(string? apiKey)
{
    [JsonPropertyName("api_key")]
    public string? ApiKey { get; set; } = apiKey;
}
