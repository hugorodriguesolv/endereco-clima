namespace EnderecoClima.Infrastructure.Providers.ViaCep;

public sealed class ViaCepOptions
{
    public const string SectionName = "Providers:ViaCep";
    public string BaseUrl { get; init; } = "https://viacep.com.br";
}