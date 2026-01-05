using EnderecoClima.Infrastructure.Dtos.Providers;

namespace EnderecoClima.Infrastructure.Extensions.Mappings;

public static class ZipCodeLookupMapping
{
    public static ZipCodeLookupDto ToZipCodeLookup(this BrasilApiCepV2ResponseDto result)
        => new(ZipCode: result.Cep ?? "00000-00",
                Street: result.Street,
                District: result.Neighborhood,
                City: result.City,
                State: result.State,
                Ibge: null,
                Location: result.Location is null ? null : new LocationDto(result.Location.Latitude, result.Location.Longitude),
                Provider: "BrasilApiV2");

    public static ZipCodeLookupDto ToZipCodeLookup(this ViaCepResponseDto result)
        => new(ZipCode: result.Cep ?? "00000-00",
                Street: result.Logradouro,
                District: result.Bairro,
                City: result.Localidade,
                State: result.Uf,
                Ibge: result.Ibge,
                Location: null,
                Provider: "ViaCep");
}