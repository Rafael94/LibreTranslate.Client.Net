# LibreTranslate.Client.Net

LibreTranslate.Client.Net is a unofficial C# client (netstandard2.1) for using the rest api of [LibreTranslate](https://github.com/LibreTranslate/LibreTranslate). Unfortunately, netstandard2.0 cannot be supported because HttpStatusCode.TooManyRequests is missing in netstandard2.1. All functions of LibreTranslate (v1.6.1) are supported.
This library supports all the LibreTranslate languages. For ease of use, some language code is stored in `LanguageCode`.

The library uses the latest technologies. Source code generators for logging and serialization and deserialization for JSON. It can also be used with HTTPClientFactory and DependencyInjection.

The result pattern is used to return the client.

## Using

### Instllation

```
Install-Package LibreTranslate.Client.Net 
```

### Direct

```csharp
using LibreTranslate.Client.Net;
using LibreTranslate.Client.Net.Models;

using LibreTranslateClient client = new(new LibreTranslateClientOptions("https//localhost:5000"));

LibreTranslateApiResult<TranslateResponse> translateResult = client.TranslateAsync("Bei C# handelt es sich um eine der besten Programmiersprachen der Welt.", LanguageCode.German, LanguageCodes.English);

if (translateResult.IsSuccess == false)
{
    throw new Exception(translateResult.Error!.Error);
}

Console.WriteLine(result.Value!.TranslatedText);

```

### DependencyIncection, HttpClientFactory

Service registration:

```csharp
public static void ConfigureServices(IServiceCollection services)
{
 services.Configure<LibreTranslateClientOptions>(configuration.GetSection("LibreTranslate"));

 services.AddHttpClient();

 services.AddScoped<ILibreTranslateClient>(services =>
 {
     return new LibreTranslateClient(
         client: services.GetRequiredService<HttpClient>(),
         options: services.GetRequiredService<IOptions<LibreTranslateClientOptions>>().Value,
         logger: services.GetRequiredService<ILogger<LibreTranslateClient>>()
         );
 });
}
```
Using in a class:

```csharp
public class MyClass(LibreTranslateClient translationClient) {
}
```

### Docker

LibreTranslate can be used in Docker for testing. The corresponding Docker-Compose is located in the root directory of the project.

```
docker-compose up -d
```
CUDA
```
docker-compose -f docker-compose.cuda.yml up -d
```

## Methods

```csharp
Task<LibreTranslateApiResult<DetectResponse>> DetectAsync(string text, CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<LanguageResponse>> GetSupportedLanguagesAsync(CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<TranslateResponse>> TranslateAsync(string q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<TranslateMultipleResponse>> TranslateAsync(IEnumerable<string> q, string sourceCode, string targetCode, string format = "text", int alternatives = 1, CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<TranslateFileResponse>> TranslateFileAsync(byte[] file, string fileName, string sourceCode, string targetCode, CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<FrontendSettingsResponse>> GetFrontendSettingsAsync(CancellationToken cancellationToken = default);
Task<LibreTranslateApiResult<SuggestResponse>> SuggestAsync(string q, string suggestion, string sourceCode, string targetCode, CancellationToken cancellationToken = default);
```