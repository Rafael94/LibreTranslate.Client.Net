using System.Collections.Generic;
using System.Linq;

namespace LibreTranslate.Client.Net.Models;

public sealed class TranslateMultipleRequest(string? apiKey, IEnumerable<string> q, string sourceCode, string targetCode) : BaseRequest(apiKey)
{

    /// <summary>
    /// Zu übersetzende Texte
    /// </summary>
    public IEnumerable<string> Q { get; set; } = q;

    /// <summary>
    /// Quellsprachen-Code
    /// </summary>
    public string Source { get; set; } = sourceCode;

    /// <summary>
    /// Zielsprachen-Code
    /// </summary>
    public string Target { get; set; } = targetCode;

    /// <summary>
    /// Api version 1.6.1 only allows text and html
    /// Default: text
    /// </summary>
    public string Format { get; set; } = "text";

    /// <summary>
    /// Bevorzugte Anzahl alternativer Übersetzungen
    /// Default: 1
    /// </summary>
    public int Alternatives { get; set; } = 1;
}
