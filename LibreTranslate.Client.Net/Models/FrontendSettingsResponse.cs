using System.Collections.Generic;

namespace LibreTranslate.Client.Net.Models;

/// <summary>
/// Front-end specific settings
/// </summary>
public sealed class FrontendSettingsResponse
{
    /// <summary>
    /// Whether the API key database is activated.
    /// </summary>
    public bool ApiKeys { get; set; }
    /// <summary>
    /// Character input limit for this language (-1 means unlimited)
    /// </summary>
    public int CharLimit { get; set; }
    public bool FilesTranslation { get; set; }
    /// <summary>
    /// Timeout of the front-end translation
    /// </summary>
    public int FrontendTimeout { get; set; }
    /// <summary>
    /// Whether an API key is required.
    /// </summary>
    public bool KeyRequired { get; set; }
    public FrontendSettingsResponseLanguage? Language { get; set; } = default!;
    /// <summary>
    /// Whether the submission of proposals is activated.
    /// </summary>
    public bool Suggestions { get; set; }
    /// <summary>
    /// Supported file format
    /// </summary>
    public List<string> SupportedFilesFormat { get; set; } = default!;

    public class FrontendSettingsResponseLanguage
    {
        /// <summary>
        /// <see cref="LanguageCodes"/>
        /// </summary>
        public string Source { get; set; } = default!;
        /// <summary>
        /// <see cref="LanguageCodes"/>
        /// </summary>
        public string Target { get; set; } = default!;
    }
}
