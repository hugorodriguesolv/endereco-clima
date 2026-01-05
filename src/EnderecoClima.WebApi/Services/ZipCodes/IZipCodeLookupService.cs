using EnderecoClima.Infrastructure.Dtos.Providers;

namespace EnderecoClima.WebApi.Services.ZipCodes;

public interface IZipCodeLookupService
{
    Task<ZipCodeLookupDto> LookupAsync(string zipCode8Digits, CancellationToken ct);
}