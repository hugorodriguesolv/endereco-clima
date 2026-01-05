using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Extensions.Mappings;
using EnderecoClima.Infrastructure.Interfaces.Providers;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public sealed class ZipCodeLookupService(
    IBrasilApiCepV2Provider brasilApiCepV2Provider,
    IViaCepProvider viaCepProvider) : IZipCodeLookupService
{
    public async Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct)
    {
        //var resultBrasilApi = await brasilApiCepV2Provider.TryGetAsync(zipCode8Digits, ct);
        //if (resultBrasilApi is null) throw new KeyNotFoundException($"CEP {zipCode8Digits} não encontrado.");

        var resultViaCep = await viaCepProvider.TryGetAsync(zipCode8Digits, ct);
        if (resultViaCep is null) throw new KeyNotFoundException($"CEP {zipCode8Digits} não encontrado.");

        return resultViaCep.ToZipCodeLookup();
    }
}