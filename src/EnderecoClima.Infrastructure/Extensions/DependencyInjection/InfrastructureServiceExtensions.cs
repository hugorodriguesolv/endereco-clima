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
          .Validate(o => Uri.TryCreate(o.BaseUrl, UriKind.Absolute, out _), "BrasilApi: BaseUrl inválida");

        services.AddHttpClient<IBrasilApiCepV2Provider, BrasilApiCepV2Provider>((sp, http) =>
        {
            var opt = sp.GetRequiredService<IOptions<BrasilApiCepOptions>>().Value;
            http.BaseAddress = new Uri(opt.BaseUrl);

            // Timeout controlado pelo pipeline de resiliência
            http.Timeout = Timeout.InfiniteTimeSpan;
        })
        .AddStandardResilienceHandler(configuration.GetSection("HttpResilience:BrasilApi"));

        // ViaCEP
        services.AddOptions<ViaCepOptions>()
          .Bind(configuration.GetSection(ViaCepOptions.SectionName))
          .Validate(o => Uri.TryCreate(o.BaseUrl, UriKind.Absolute, out _), "ViaCep: BaseUrl inválida");

        services.AddHttpClient<IViaCepProvider, ViaCepProvider>((sp, http) =>
        {
            var opt = sp.GetRequiredService<IOptions<ViaCepOptions>>().Value;
            http.BaseAddress = new Uri(opt.BaseUrl);

            http.Timeout = Timeout.InfiniteTimeSpan;
        })
        .AddStandardResilienceHandler(configuration.GetSection("HttpResilience:ViaCep"));

        return services;
    }
}