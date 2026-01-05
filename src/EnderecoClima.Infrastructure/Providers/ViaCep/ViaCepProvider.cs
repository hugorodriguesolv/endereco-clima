using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

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

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            logger.LogWarning("ViaCEP falhou. Status={Status}. Body={Body}", (int)response.StatusCode, body);
            response.EnsureSuccessStatusCode();
        }

        var payload = await response.Content.ReadFromJsonAsync<ViaCepResponseDto>(cancellationToken: ct)
            ?? throw new HttpRequestException("ViaCEP retornou payload vazio.");

        if (payload.Erro is true)
        {
            logger.LogInformation("ViaCEP: CEP {ZipCode} não encontrado (erro=true)", normalizedZipCode);
            return null;
        }

        logger.LogInformation("ViaCEP atendeu CEP {ZipCode}", normalizedZipCode);

        return payload;
    }
}