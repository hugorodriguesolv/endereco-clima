using EnderecoClima.Infrastructure.Interfaces.Providers;
using EnderecoClima.Infrastructure.Providers.BrasilApi;
using EnderecoClima.Infrastructure.Providers.ViaCep;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EnderecoClima.Infrastructure.Extensions.DependencyInjection;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
    {
        // BrasilAPI
        services.AddOptions<BrasilApiCepOptions>()
          .Bind(configuration.GetSection(BrasilApiCepOptions.SectionName))
          .Validate(o => Uri.TryCreate(o.BaseUrl, UriKind.Absolute, out _), "BrasilApi: BaseUrl inválida")
          .Validate(o => o.TimeoutSeconds is >= 1 and <= 200, "BrasilApi: TimeoutSeconds deve estar entre 1 e 10");

        services.AddHttpClient<IBrasilApiCepV2Provider, BrasilApiCepV2Provider>((sp, http) =>
        {
            var options = sp.GetRequiredService<IOptions<BrasilApiCepOptions>>().Value;
            http.BaseAddress = new Uri(options.BaseUrl);
            http.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        // ViaCEP
        services.AddOptions<ViaCepOptions>()
          .Bind(configuration.GetSection(ViaCepOptions.SectionName))
          .Validate(o => Uri.TryCreate(o.BaseUrl, UriKind.Absolute, out _), "ViaCep: BaseUrl inválida")
          .Validate(o => o.TimeoutSeconds is >= 1 and <= 200, "ViaCep: TimeoutSeconds deve estar entre 1 e 10");

        services.AddHttpClient<IViaCepProvider, ViaCepProvider>((sp, http) =>
        {
            var options = sp.GetRequiredService<IOptions<ViaCepOptions>>().Value;
            http.BaseAddress = new Uri(options.BaseUrl);
            http.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        });

        return services;
    }
}