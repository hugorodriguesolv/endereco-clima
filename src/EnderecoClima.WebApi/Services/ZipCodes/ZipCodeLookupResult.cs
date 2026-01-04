namespace EnderecoClima.WebApi.Services.ZipCodes;

public sealed record ZipCodeLocation(double Lat, double Lon);

public sealed record ZipCodeLookupResult(
  string ZipCode,
  string? Street,
  string? District,
  string? City,
  string? State,
  string? Ibge,
  ZipCodeLocation? Location,
  string Provider);

public interface IZipCodeLookupService
{
    Task<ZipCodeLookupResult> LookupAsync(string zipCode8Digits, CancellationToken ct);
}