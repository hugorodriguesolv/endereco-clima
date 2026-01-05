namespace EnderecoClima.Infrastructure.Providers.BrasilApi;

public sealed class BrasilApiCepOptions
{
    public const string SectionName = "Providers:BrasilApi";
    public string BaseUrl { get; init; } = "https://brasilapi.com.br/";
}