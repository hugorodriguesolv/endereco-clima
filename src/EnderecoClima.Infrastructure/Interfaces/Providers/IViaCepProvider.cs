using EnderecoClima.Infrastructure.Dtos.Providers;

namespace EnderecoClima.Infrastructure.Interfaces.Providers;

public interface IViaCepProvider
{
    Task<ViaCepResponseDto?> TryGetAsync(string normalizedZipCode, CancellationToken ct);
}