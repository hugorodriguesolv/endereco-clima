using EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public interface IZipCodeLookupService
{
    Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct);
}