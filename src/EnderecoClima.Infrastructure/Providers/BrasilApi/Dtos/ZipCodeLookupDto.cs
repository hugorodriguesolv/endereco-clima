using System;
using System.Collections.Generic;
using System.Text;

namespace EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos;

public sealed record ZipCodeLookupDto(
  string ZipCode,
  string? Street,
  string? District,
  string? City,
  string? State,
  string? Ibge,
  LocationDto? Location,
  string Provider);

public sealed record LocationDto(double Lat, double Lon);