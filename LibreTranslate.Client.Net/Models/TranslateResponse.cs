using System.Collections.Generic;

namespace LibreTranslate.Client.Net.Models;

public sealed class TranslateResponse
{
    public string TranslatedText { get; set; } = default!;
    public List<string>? Alternatives { get; set; }
}
