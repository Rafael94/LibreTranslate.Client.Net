using System.Text.Json.Serialization;

using LibreTranslate.Client.Net.Models;

namespace LibreTranslate.Client.Net;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(BaseRequest))]
[JsonSerializable(typeof(DetectRequest))]
[JsonSerializable(typeof(DetectResponse))]
[JsonSerializable(typeof(LanguageResponse))]
[JsonSerializable(typeof(TranslateRequest))]
[JsonSerializable(typeof(TranslateMultipleRequest))]
[JsonSerializable(typeof(TranslateResponse))]
[JsonSerializable(typeof(TranslateMultipleResponse))]
[JsonSerializable(typeof(TranslateFileResponse))]
[JsonSerializable(typeof(FrontendSettingsResponse))]
[JsonSerializable(typeof(SuggestRequest))]
[JsonSerializable(typeof(SuggestResponse))]
[JsonSerializable(typeof(LibreTranslateApiError))]
public sealed partial class LibreTranslatorJsonSerializerContext : JsonSerializerContext
{
}
