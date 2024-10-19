using System.Text.Json.Serialization;

namespace LibreTranslate.Client.Net.Models;

public sealed class SuggestRequest(string? apiKey, string q, string suggestion, string source, string target) : BaseRequest(apiKey)
{
    /// <summary>
    /// Ursprünglicher Text
    /// </summary>
    public string Q { get; set; } = q;

    /// <summary>
    /// Vorgeschlagene Übersetzung
    /// </summary>
    [JsonPropertyName("s")]
    public string Suggestion { get; set; } = suggestion;

    /// <summary>
    /// Sprache des Originaltextes
    /// </summary>
    public string Source { get; set; } = source;

    /// <summary>
    /// Sprache der vorgeschlagenen Übersetzung
    /// </summary>
    public string Target { get; set; } = target;
}
