using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using LibreTranslate.Client.Net.Models;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace LibreTranslate.Client.Net;

public partial class LibreTranslateClient : IDisposable, ILibreTranslateClient
{
    protected readonly HttpClient _client;

    /// <summary>
    /// Wenn intern erstellt, muss der <see cref="_client"/> bei Dispose freigegeben werden
    /// </summary>
    private readonly bool _externalClient = false;

    protected readonly LibreTranslateClientOptions _options;
    protected readonly ILogger _logger;

    /// <summary>
    /// Erstellt einen internen HttpClient
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    public LibreTranslateClient(LibreTranslateClientOptions options, ILogger? logger)
    {
        _options = options;
        _client = new HttpClient();
        _logger = logger ?? NullLogger.Instance;
    }

    /// <summary>
    /// Für die Verwendung von wiederverndbaren HttpClient
    /// </summary>
    /// <param name="client"></param>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    public LibreTranslateClient(HttpClient client, LibreTranslateClientOptions options, ILogger? logger)
    {
        _client = client;
        _options = options;
        _externalClient = true;
        _logger = logger ?? NullLogger.Instance;
    }

    public virtual void Dispose()
    {
        if (_externalClient == false)
        {
            _client.Dispose();
        }
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<DetectResponse>> DetectAsync(string text, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _client.PostAsJsonAsync($"{_options.BaseAddress}detect", new DetectRequest(_options.ApiKey, text), LibreTranslatorJsonSerializerContext.Default.DetectRequest, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.DetectResponse, cancellationToken))!;
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Forbidden:
#if NETSTANDARD2_0
            case (HttpStatusCode)429:
#else
            case HttpStatusCode.TooManyRequests:
#endif
            case HttpStatusCode.InternalServerError:
                LibreTranslateApiError error = (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError, cancellationToken))!;
                error.StatusCode = response.StatusCode;

                LogError($"{_options.BaseAddress}languages", response.StatusCode, error.Error);

                return error;
            default:
                LogUnhandledError($"{_options.BaseAddress}detect", response.StatusCode, await response.Content.ReadAsStringAsync());
                return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
        }
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<LanguageResponse>> GetSupportedLanguagesAsync(CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_options.BaseAddress}languages"),
            Content = new StringContent(
                JsonSerializer.Serialize(new BaseRequest(_options.ApiKey), LibreTranslatorJsonSerializerContext.Default.BaseRequest),
                Encoding.UTF8,
                "application/json"
                )
        };

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LanguageResponse, cancellationToken))!;
        }

        LogUnhandledError($"{_options.BaseAddress}languages", response.StatusCode, await response.Content.ReadAsStringAsync());
        return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<TranslateResponse>> TranslateAsync(string q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _client.PostAsJsonAsync($"{_options.BaseAddress}translate",
            new TranslateRequest(_options.ApiKey, q, sourceCode, targetCode)
            {
                Alternatives = alternatives,
                Format = format,
            },
            LibreTranslatorJsonSerializerContext.Default.TranslateRequest, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.TranslateResponse, cancellationToken))!;
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Forbidden:
#if NETSTANDARD2_0
            case (HttpStatusCode)429:
#else
            case HttpStatusCode.TooManyRequests:
#endif
            case HttpStatusCode.InternalServerError:
                LibreTranslateApiError error = (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError, cancellationToken))!;
                error.StatusCode = response.StatusCode;

                LogError($"{_options.BaseAddress}translate", response.StatusCode, error.Error);

                return error;
            default:
                LogUnhandledError($"{_options.BaseAddress}translate", response.StatusCode, await response.Content.ReadAsStringAsync());
                return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
        }
    }

    public virtual async Task<LibreTranslateApiResult<TranslateMultipleResponse>> TranslateAsync(IEnumerable<string> q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _client.PostAsJsonAsync($"{_options.BaseAddress}translate",
                new TranslateMultipleRequest(_options.ApiKey, q, sourceCode, targetCode)
                {
                    Alternatives = alternatives,
                    Format = format,
                },
                LibreTranslatorJsonSerializerContext.Default.TranslateMultipleRequest, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.TranslateMultipleResponse, cancellationToken))!;
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Forbidden:
#if NETSTANDARD2_0
            case (HttpStatusCode)429:
#else
            case HttpStatusCode.TooManyRequests:
#endif
            case HttpStatusCode.InternalServerError:
                LibreTranslateApiError error = (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError, cancellationToken))!;
                error.StatusCode = response.StatusCode;

                LogError($"{_options.BaseAddress}translate", response.StatusCode, error.Error);

                return error;
            default:
                LogUnhandledError($"{_options.BaseAddress}translate", response.StatusCode, await response.Content.ReadAsStringAsync());
                return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
        }
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<TranslateFileResponse>> TranslateFileAsync(byte[] file, string fileName, string sourceCode, string targetCode, CancellationToken cancellationToken = default)
    {
        using MultipartFormDataContent content = [];
        using MemoryStream fileStream = new(file);

        using StreamContent fileContent = new(fileStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
        content.Add(fileContent, "file", fileName);

        using StringContent apiKeyContent = new(_options.ApiKey);
        content.Add(apiKeyContent, "api_key");

        using StringContent sourceContent = new(sourceCode);
        content.Add(sourceContent, "source");

        using StringContent targetContent = new(targetCode);
        content.Add(targetContent, "target");

        using HttpResponseMessage response = await _client.PostAsync($"{_options.BaseAddress}translate_file", content, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.TranslateFileResponse, cancellationToken))!;
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Forbidden:
#if NETSTANDARD2_0
            case (HttpStatusCode)429:
#else
            case HttpStatusCode.TooManyRequests:
#endif
            case HttpStatusCode.InternalServerError:
                LibreTranslateApiError error = (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError, cancellationToken))!;
                error.StatusCode = response.StatusCode;

                LogError($"{_options.BaseAddress}translate_file", response.StatusCode, error.Error);

                return error;
            default:
                LogUnhandledError($"{_options.BaseAddress}translate_file", response.StatusCode, await response.Content.ReadAsStringAsync());
                return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
        }
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<FrontendSettingsResponse>> GetFrontendSettingsAsync(CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{_options.BaseAddress}frontend/settings"),
            Content = new StringContent(
               JsonSerializer.Serialize(new BaseRequest(_options.ApiKey), LibreTranslatorJsonSerializerContext.Default.BaseRequest),
               Encoding.UTF8,
               "application/json"
               )
        };

        using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.FrontendSettingsResponse, cancellationToken))!;
        }

        LogUnhandledError($"{_options.BaseAddress}frontend/settings", response.StatusCode, await response.Content.ReadAsStringAsync());
        return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
    }

    /// <inheritdoc/>
    public virtual async Task<LibreTranslateApiResult<SuggestResponse>> SuggestAsync(string q, string suggestion, string sourceCode, string targetCode, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await _client.PostAsJsonAsync($"{_options.BaseAddress}suggest",
              new SuggestRequest(_options.ApiKey, q, suggestion, sourceCode, targetCode),
              LibreTranslatorJsonSerializerContext.Default.SuggestRequest, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.SuggestResponse, cancellationToken))!;
            case HttpStatusCode.Forbidden:
                LibreTranslateApiError error = (await response.Content.ReadFromJsonAsync(LibreTranslatorJsonSerializerContext.Default.LibreTranslateApiError, cancellationToken))!;
                error.StatusCode = response.StatusCode;

                LogError($"{_options.BaseAddress}suggest", response.StatusCode, error.Error);
                return error;
            default:
                LogUnhandledError($"{_options.BaseAddress}suggest", response.StatusCode, await response.Content.ReadAsStringAsync());
                return new LibreTranslateApiError($"StatusCode: ${response.StatusCode}", response.StatusCode);
        }
    }

    [LoggerMessage(LogLevel.Error, "Url: {url}, StatusCode: {statusCode}, Api-Response: {response}")]
    protected partial void LogError(string url, HttpStatusCode statusCode, string response);

    [LoggerMessage(LogLevel.Error, "Undocumented error: Url: {url}, StatusCode: {statusCode}, Api-Response: {response}")]
    protected partial void LogUnhandledError(string url, HttpStatusCode statusCode, string response);
}
