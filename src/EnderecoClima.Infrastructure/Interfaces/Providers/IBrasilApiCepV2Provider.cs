using EnderecoClima.Infrastructure.Dtos.Providers;

namespace EnderecoClima.Infrastructure.Interfaces.Providers;

public interface IBrasilApiCepV2Provider
{
    Task<BrasilApiCepV2ResponseDto?> TryGetAsync(string normalizedZipCode, CancellationToken ct);
}