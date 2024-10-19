namespace LibreTranslate.Client.Net.Models;

public sealed class DetectRequest(string? apiKey, string q) : BaseRequest(apiKey)
{
    public string Q { get; set; } = q;
}
