using EnderecoClima.Core.ZipCodeLookupAggregate.DomainObjects;

namespace EnderecoClima.Core.ZipCodeLookupAggregate;

public sealed class ZipCodeLookup
{
    public Guid Id { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public Street Street { get; private set; }
    public District District { get; private set; }
    public City City { get; private set; }
    public State State { get; private set; }
    public Ibge Ibge { get; private set; }
    public Location Location { get; private set; }
    public Provider Provider { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    private ZipCodeLookup()
    { }

    private ZipCodeLookup(
      Guid id,
      ZipCode zipCode,
      Street street,
      District district,
      City city,
      State state,
      Ibge ibge,
      Location location,
      Provider provider,
      DateTimeOffset createdAtUtc)
    {
        Id = id;
        ZipCode = zipCode;
        Street = street;
        District = district;
        City = city;
        State = state;
        Ibge = ibge;
        Location = location;
        Provider = provider;
        CreatedAtUtc = createdAtUtc;
    }

    public static ZipCodeLookup Create(
      ZipCode zipCode,
      Street street,
      District district,
      City city,
      State state,
      Ibge ibge,
      Location location,
      Provider provider,
      DateTimeOffset createdAtUtc)
      => new(Guid.NewGuid(), zipCode, street, district, city, state, ibge, location, provider, createdAtUtc);
}