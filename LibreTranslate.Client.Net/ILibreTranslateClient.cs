using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using LibreTranslate.Client.Net.Models;

namespace LibreTranslate.Client.Net;

public interface ILibreTranslateClient
{
    /// <summary>
    /// Recognize the language of a single text
    /// </summary>
    /// <param name="text">Zu erkennender Text</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<DetectResponse>> DetectAsync(string text, CancellationToken cancellationToken = default);

    /// <summary>
    /// Call up the list of supported languages
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<LanguageResponse>> GetSupportedLanguagesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Translating text from one language into another
    /// </summary>
    /// <param name="q">Zu übersetzender Text</param>
    /// <param name="sourceCode">Quellsprachen-Code</param>
    /// <param name="targetCode">Zielsprachen-Code</param>
    /// <param name="format">Api version 1.6.1 only allows text and html</param>
    /// <param name="alternatives">Bevorzugte Anzahl alternativer Übersetzungen</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<TranslateResponse>> TranslateAsync(string q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default);

    /// <summary>
    /// Translating text from one language into another
    /// </summary>
    /// <param name="q">Zu übersetzende Texte</param>
    /// <param name="sourceCode">Quellsprachen-Code</param>
    /// <param name="targetCode">Zielsprachen-Code</param>
    /// <param name="format">Api version 1.6.1 only allows text and html</param>
    /// <param name="alternatives">Bevorzugte Anzahl alternativer Übersetzungen</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<TranslateMultipleResponse>> TranslateAsync(IEnumerable<string> q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default);

    /// <summary>
    /// Translate file from one language to another
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileName"></param>
    /// <param name="sourceCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<TranslateFileResponse>> TranslateFileAsync(byte[] file, string fileName, string sourceCode, string targetCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieving frontend-specific settings
    /// </summary>
    /// <returns></returns>
    Task<LibreTranslateApiResult<FrontendSettingsResponse>> GetFrontendSettingsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Submit a suggestion for improving a translation
    /// </summary>
    /// <param name="q"></param>
    /// <param name="suggestion"></param>
    /// <param name="sourceCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<LibreTranslateApiResult<SuggestResponse>> SuggestAsync(string q, string suggestion, string sourceCode, string targetCode, CancellationToken cancellationToken = default);
}
