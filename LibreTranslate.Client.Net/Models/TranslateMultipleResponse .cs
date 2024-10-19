using System.Collections.Generic;

namespace LibreTranslate.Client.Net.Models;

public sealed class TranslateMultipleResponse
{
    public List<string> TranslatedText { get; set; } = default!;
    public List<List<string>>? Alternatives { get; set; }
}
