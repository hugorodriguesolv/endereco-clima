namespace EnderecoClima.Infrastructure.Providers.BrasilApi.Dtos
{
    public sealed class BrasilApiCepV2ResponseDto
    {
        public string? Cep { get; init; }
        public string? State { get; init; }
        public string? City { get; init; }
        public string? Neighborhood { get; init; }
        public string? Street { get; init; }
        public string? Service { get; init; }
        public BrasilApiLocation? Location { get; init; }

        public sealed class BrasilApiLocation
        {
            public string? Type { get; init; }
            public BrasilApiCoordinates? Coordinates { get; init; }
        }

        public sealed class BrasilApiCoordinates
        {
            public string? Latitude { get; init; }

            public string? Longitude { get; init; }
        }
    }
}