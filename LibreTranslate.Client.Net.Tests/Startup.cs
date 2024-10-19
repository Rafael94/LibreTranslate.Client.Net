using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LibreTranslate.Client.Net.Tests;

public class Startup
{
    /// <summary>
    /// Configure DependencyInjection for xunit tests
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServices(IServiceCollection services)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"AppSettings.json", false, false)
               .AddUserSecrets<Startup>()
               .AddEnvironmentVariables()
               .Build();

        services
            .AddSingleton<IConfiguration>(configuration)
            .AddLogging();

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
}
