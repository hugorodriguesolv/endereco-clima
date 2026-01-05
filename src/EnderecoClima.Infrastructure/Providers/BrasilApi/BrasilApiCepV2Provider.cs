using EnderecoClima.Infrastructure.Dtos.Providers;
using EnderecoClima.Infrastructure.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace EnderecoClima.Infrastructure.Providers.BrasilApi;

internal sealed class BrasilApiCepV2Provider(HttpClient http, ILogger<BrasilApiCepV2Provider> logger)
  : IBrasilApiCepV2Provider
{
    public async Task<BrasilApiCepV2ResponseDto?> TryGetAsync(string normalizedZipCode, CancellationToken ct)
    {
        var path = $"api/cep/v2/{normalizedZipCode}";

        using var response = await http.GetAsync(path, ct);

        if (response.StatusCode is HttpStatusCode.NotFound)
        {
            logger.LogInformation("BrasilAPI: CEP {ZipCode} não encontrado", normalizedZipCode);
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            logger.LogWarning("BrasilAPI falhou. Status={Status}. Body={Body}", (int)response.StatusCode, body);
            response.EnsureSuccessStatusCode();
        }

        var payload = await response.Content.ReadFromJsonAsync<BrasilApiCepV2ResponseDto>(cancellationToken: ct)
            ?? throw new HttpRequestException("BrasilAPI retornou payload vazio.");

        logger.LogInformation("BrasilAPI atendeu CEP {ZipCode}", normalizedZipCode);

        return payload;
    }
}