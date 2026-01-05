using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Extensions.Mappings;
using EnderecoClima.Infrastructure.Interfaces.Providers;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public sealed class ZipCodeLookupService(IBrasilApiCepV2Provider brasilApi) : IZipCodeLookupService
{
    public async Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct)
    {
        var result = await brasilApi.TryGetAsync(zipCode8Digits, ct);
        if (result is null) throw new KeyNotFoundException($"CEP {zipCode8Digits} não encontrado.");

        return result.ToZipCodeLookup();
    }
}