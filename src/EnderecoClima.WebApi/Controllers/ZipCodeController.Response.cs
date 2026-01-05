namespace EnderecoClima.WebApi.Controllers;

public sealed partial class ZipCodesController
{
    public sealed record LocationResponse(double? Lat, double? Lon);

    public sealed record AdressResponse(
      string ZipCode,
      string? Street,
      string? District,
      string? City,
      string? State,
      string? Ibge,
      LocationResponse? Location,
      string Provider);
}