using EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;

namespace EnderecoClima.Infrastructure.Interfaces.Providers;

public interface IBrazilApiZipCodeProvider
{
    Task<ZipCodeLookupDto?> TryGetAsync(string normalizedZipCode, CancellationToken ct);
}