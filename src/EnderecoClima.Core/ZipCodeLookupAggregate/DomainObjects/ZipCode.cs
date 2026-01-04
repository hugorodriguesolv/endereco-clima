using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace EnderecoClima.Core.ZipCodeLookupAggregate.DomainObjects;

public readonly partial record struct ZipCode
{
    private static readonly Regex NonDigits = NonDigitsRegex();

    public string Value { get; }

    private ZipCode(string normalized8Digits) => Value = normalized8Digits;

    public override string ToString() => Value;

    public static ZipCode Create(string? input)
    {
        if (!TryCreate(input, out var zip))
            throw new ArgumentException("CEP deve conter 8 dígitos.", "zipCode");

        return zip;
    }

    public static bool TryCreate(string? input, [NotNullWhen(true)] out ZipCode zipCode)
    {
        zipCode = default;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        var normalized = Normalize(input);

        if (normalized.Length != 8)
            return false;

        zipCode = new ZipCode(normalized);
        return true;
    }

    public static string Normalize(string input)
      => NonDigits.Replace(input, string.Empty);

    [GeneratedRegex(@"\D+", RegexOptions.Compiled)]
    private static partial Regex NonDigitsRegex();
}