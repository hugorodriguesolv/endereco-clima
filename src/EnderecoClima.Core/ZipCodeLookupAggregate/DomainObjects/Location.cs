namespace EnderecoClima.Core.ZipCodeLookupAggregate.DomainObjects;

public readonly record struct Location
{
    public double Lat { get; }
    public double Lon { get; }

    private Location(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }

    public static Location Create(double lat, double lon)
    {
        // Validação mínima e segura (sem exagero)
        if (lat is < -90 or > 90)
            throw new ArgumentOutOfRangeException("Latitude inválida. Intervalo permitido: -90 a 90.");

        if (lon is < -180 or > 180)
            throw new ArgumentOutOfRangeException("Longitude inválida. Intervalo permitido: -180 a 180.");

        return new Location(lat, lon);
    }
}