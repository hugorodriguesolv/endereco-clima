using EnderecoClima.Infrastructure.Interfaces.Providers;
using EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public sealed class ZipCodeLookupService(IBrazilApiZipCodeProvider brasilApi) : IZipCodeLookupService
{
    public async Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct)
    {
        var result = await brasilApi.TryGetAsync(zipCode8Digits, ct);
        if (result is null) throw new KeyNotFoundException($"CEP {zipCode8Digits} não encontrado.");

        return result;
    }
}