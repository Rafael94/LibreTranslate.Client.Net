using System.Collections.Generic;

namespace LibreTranslate.Client.Net.Models;

public sealed class LanguageResponse : List<LanguageDto>
{
}

public sealed class LanguageDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public List<string>? Targets { get; set; }

    public override string ToString() => $"{Name} ({Code})";
}