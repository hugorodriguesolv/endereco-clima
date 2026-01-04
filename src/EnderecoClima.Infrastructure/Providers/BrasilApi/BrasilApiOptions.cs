using System;
using System.Collections.Generic;
using System.Text;

namespace EnderecoClima.Infrastructure.Providers.BrasilApi;

public sealed class BrasilApiOptions
{
    public const string SectionName = "Providers:BrasilApi";

    public string BaseUrl { get; init; } = "https://brasilapi.com.br/";
    public int TimeoutSeconds { get; init; } = 200;
}