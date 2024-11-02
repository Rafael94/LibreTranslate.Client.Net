namespace LibreTranslate.Client.Net;

public sealed class LibreTranslateClientOptions
{
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
    public LibreTranslateClientOptions(string baseAddress, string? apiKey)
    {
        BaseAddress = baseAddress;
        ApiKey = apiKey;
    }

    public LibreTranslateClientOptions() { }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.


    private string _baseAddress;
    public string BaseAddress
    {
        get { return _baseAddress; }
        set
        {
#if (NETSTANDARD2_0)
            if (value.EndsWith("/", System.StringComparison.Ordinal))
#else
            if (value.EndsWith('/'))
#endif
            {
                _baseAddress = value;
            }
            else
            {
                _baseAddress = value + '/';
            }
        }
    }
    public string? ApiKey { get; set; }
}
