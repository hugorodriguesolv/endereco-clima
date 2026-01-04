namespace EnderecoClima.WebApi.Controllers;

public sealed partial class ZipCodesController
{
    public sealed record LocationDto(double Lat, double Lon);

    public sealed record AdressDto(
      string ZipCode,
      string? Street,
      string? District,
      string? City,
      string? State,
      string? Ibge,
      LocationDto? Location,
      string Provider);
}