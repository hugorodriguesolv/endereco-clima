using System.Text.Json.Serialization;

namespace EnderecoClima.Infrastructure.Dtos.Providers;

public sealed record ViaCepResponseDto(
  [property: JsonPropertyName("cep")] string? Cep,
  [property: JsonPropertyName("logradouro")] string? Logradouro,
  [property: JsonPropertyName("bairro")] string? Bairro,
  [property: JsonPropertyName("localidade")] string? Localidade,
  [property: JsonPropertyName("uf")] string? Uf,
  [property: JsonPropertyName("ibge")] string? Ibge,
  [property: JsonPropertyName("erro")] bool? Erro
);