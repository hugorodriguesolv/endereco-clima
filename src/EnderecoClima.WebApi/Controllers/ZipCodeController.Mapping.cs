using EnderecoClima.Infrastructure.Dtos.Providers;

namespace EnderecoClima.WebApi.Controllers;

public static class ZipCodesMapping
{
    public static ZipCodesController.AdressResponse ToGetResponse(this ZipCodeLookupDto result)
         => new(ZipCode: result.ZipCode,
             Street: result.Street,
             District: result.District,
             City: result.City,
             State: result.State,
             Ibge: result.Ibge,
             Location: result.Location is null ? null : new ZipCodesController.LocationResponse(result.Location.Lat, result.Location.Lon),
             Provider: result.Provider);
}