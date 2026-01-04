using EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;
using System.Globalization;

namespace EnderecoClima.Infrastructure.Extensions.Mapper;

internal static class BrasilApiCepV2ResponseExtensions
{
    public static ZipCodeLookupDto Parser(
      this BrasilApiCepV2ResponseDto source,
      string normalizedZipCode)
    {
        return new ZipCodeLookupDto(
          ZipCode: normalizedZipCode,
          Street: source.Street,
          District: source.Neighborhood,
          City: source.City,
          State: source.State,
          Ibge: null,
          Location: source.Location.Parser(),
          Provider: "brasilapi"
        );
    }

    private static LocationDto? Parser(
      this BrasilApiCepV2ResponseDto.BrasilApiLocation? location)
    {
        var latStr = location?.Coordinates?.Latitude;
        var lonStr = location?.Coordinates?.Longitude;

        if (!double.TryParse(latStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var lat))
            return null;

        if (!double.TryParse(lonStr, NumberStyles.Float, CultureInfo.InvariantCulture, out var lon))
            return null;

        return new LocationDto(lat, lon);
    }
}