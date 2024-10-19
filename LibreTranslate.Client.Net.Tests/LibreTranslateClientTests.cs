using System.Text;

namespace LibreTranslate.Client.Net.Tests;

public sealed class LibreTranslateClientTests(ILibreTranslateClient client)
{
    [Theory]
    [InlineData("Das ist ein Beispielsatz.", LanguageCodes.German)]
    [InlineData("This is an example sentence.", LanguageCodes.English)]
    public async Task LanguageDetectTestAsync(string q, string language)
    {
        LibreTranslateApiResult<Models.DetectResponse> result = await client.DetectAsync(q);

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.True(result.Value!.Count >= 1, "No result");
            Assert.Equal(language, result.Value![0].Language);
        }
    }

    [Fact]
    public async Task SupportedLanguagesTestAsync()
    {
        LibreTranslateApiResult<Models.LanguageResponse> result = await client.GetSupportedLanguagesAsync();

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.True(result.Value!.Count > 0);
        }
    }

    [Theory]
    [InlineData("Bei C# handelt es sich um eine der besten Programmiersprachen der Welt.", LanguageCodes.German, LanguageCodes.English, "C# is one of the best programming languages in the world.")]
    public async Task TranslateAsync(string q, string sourceLanguage, string targetLanguage, string translation)
    {
        LibreTranslateApiResult<Models.TranslateResponse> result = await client.TranslateAsync(q, sourceLanguage, targetLanguage);

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.Equal(translation, result.Value!.TranslatedText);
            Assert.Single(result.Value!.Alternatives!);
        }
    }

    [Theory]
    [InlineData("Bei C# handelt es sich um eine der besten Programmiersprachen der Welt.", LanguageCodes.German, LanguageCodes.English, "C# is one of the best programming languages in the world.")]
    public async Task TranslateMultipleAsync(string q, string sourceLanguage, string targetLanguage, string translation)
    {
        LibreTranslateApiResult<Models.TranslateMultipleResponse> result = await client.TranslateAsync([q,q], sourceLanguage, targetLanguage);

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.Equal(translation, result.Value!.TranslatedText[0]);
            Assert.Equal(2, result.Value!.Alternatives!.Count);
        }
    }

    [Fact]
    public async Task TranslateFileTestAsync()
    {
        string txtContent = "Bei C# handelt es sich um eine der besten Programmiersprachen der Welt.";

        LibreTranslateApiResult<Models.TranslateFileResponse> result = await client.TranslateFileAsync(Encoding.UTF8.GetBytes(txtContent), "file.txt", LanguageCodes.German, LanguageCodes.English);

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.NotEmpty(result.Value!.TranslatedFileUrl);
        }
    }

    [Fact]
    public async Task FrontendSettingsTestAsync()
    {
        LibreTranslateApiResult<Models.FrontendSettingsResponse> result = await client.GetFrontendSettingsAsync();

        Assert.True(result.IsSuccess);

        if (result.IsSuccess)
        {
            Assert.NotNull(result.Value!.Language);
        }
    }

    [Fact]
    public async Task SuggestTestAsync()
    {
        LibreTranslateApiResult<Models.SuggestResponse> result = await client.SuggestAsync("Mandant", "Tenant", LanguageCodes.German, LanguageCodes.English);

        Assert.True(result.IsSuccess);
        if (result.IsSuccess)
        {
            Assert.True(result.Value!.Success);
        }
    }
}
