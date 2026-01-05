using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace EnderecoClima.Infrastructure.Providers.ViaCep;

internal sealed class ViaCepProvider(HttpClient http, ILogger<ViaCepProvider> logger)
  : IViaCepProvider
{
    public async Task<ViaCepResponseDto?> TryGetAsync(string normalizedZipCode, CancellationToken ct)
    {
        var path = $"ws/{normalizedZipCode}/json/";

        using var response = await http.GetAsync(path, ct);

        if (response.StatusCode is HttpStatusCode.NotFound)
        {
            logger.LogInformation("ViaCEP: CEP {ZipCode} não encontrado (404)", normalizedZipCode);
            return null;
        }

        if (response.StatusCode is HttpStatusCode.BadRequest)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            logger.LogInformation("ViaCEP: CEP inválido. Body={Body}", body);
            throw new ArgumentException("CEP inválido. Informe 8 dígitos (com ou sem hífen).", "zipCode");
        }

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            logger.LogWarning("ViaCEP falhou. Status={Status}. Body={Body}", (int)response.StatusCode, body);
            response.EnsureSuccessStatusCode();
        }

        var json = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken: ct);

        if (json.ValueKind == JsonValueKind.Object &&
            json.TryGetProperty("erro", out var erro) &&
            erro.ValueKind == JsonValueKind.String &&
            string.Equals(erro.GetString(), "true", StringComparison.OrdinalIgnoreCase))
        {
            logger.LogInformation("ViaCEP: CEP {ZipCode} não encontrado (erro=true)", normalizedZipCode);
            return null;
        }

        var payload = json.Deserialize<ViaCepResponseDto>()
            ?? throw new ValidationException("ViaCEP retornou payload inválido.");

        logger.LogInformation("ViaCEP atendeu CEP {ZipCode}", normalizedZipCode);

        return payload;
    }
}