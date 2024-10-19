using System.Net;
using System.Text.Json;

namespace LibreTranslate.Client.Net.Models;
public sealed class LibreTranslateApiError
{
    public LibreTranslateApiError()
    {
    }

    public LibreTranslateApiError(string error, HttpStatusCode? statusCode = default)
    {
        Error = error;
        StatusCode = statusCode;
    }

    public string Error { get; set; } = default!;
    public HttpStatusCode? StatusCode { get; set; }
    public static LibreTranslateApiError? FromJson(string json) => JsonSerializer.Deserialize(json, LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError);
}
