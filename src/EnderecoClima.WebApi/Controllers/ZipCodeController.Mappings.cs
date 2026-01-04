using EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;

namespace EnderecoClima.WebApi.Controllers;

public static class ZipCodesMappings
{
    public static ZipCodesController.AdressDto ToGetResponse(this ZipCodeLookupDto result)
      => new(
        ZipCode: result.ZipCode,
        Street: result.Street,
        District: result.District,
        City: result.City,
        State: result.State,
        Ibge: result.Ibge,
        Location: result.Location is null ? null : new ZipCodesController.LocationDto(result.Location.Lat, result.Location.Lon),
        Provider: result.Provider
      );
}