using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Extensions.Mappings;
using EnderecoClima.Infrastructure.Interfaces.Providers;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public sealed class ZipCodeLookupService(
    IBrasilApiCepV2Provider brasilApiCepV2Provider,
    IViaCepProvider viaCepProvider,
    ILogger<ZipCodeLookupService> logger) : IZipCodeLookupService
{
    public async Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct)
    {
        // Tenta BrasilAPI (primário)
        try
        {
            var brasil = await brasilApiCepV2Provider.TryGetAsync(zipCode8Digits, ct);
            if (brasil is not null)
                return brasil.ToZipCodeLookup();
        }
        catch (Exception ex) when (!ct.IsCancellationRequested)
        {
            // timeout
            // falha transitória já tratada pelo HttpResilience
            logger.LogWarning(ex, "Falha ao consultar BrasilAPI para CEP {ZipCode}. Tentando ViaCEP.", zipCode8Digits);
        }

        // Fallback ViaCEP
        var via = await viaCepProvider.TryGetAsync(zipCode8Digits, ct);
        if (via is not null)
            return via.ToZipCodeLookup();

        // Falha geral
        throw new KeyNotFoundException($"CEP {zipCode8Digits} não encontrado.");
    }
}