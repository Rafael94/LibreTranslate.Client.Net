using System.Collections.Generic;

namespace LibreTranslate.Client.Net.Models;

public sealed class DetectResponse: List<DetectDto>
{

}

public sealed class DetectDto
{
    public decimal Confidence { get; set; }
    public string? Language { get; set; }
}
