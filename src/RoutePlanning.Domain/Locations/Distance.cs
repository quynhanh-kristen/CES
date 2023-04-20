using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} km")]
public sealed record Distance : IValueObject
{
    public Distance(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "A distance must be greater than zero");
        }

        Value = value;
    }

    public int Value { get; private set; }

    public static implicit operator Distance(int distance) => new(distance);

    public static implicit operator int(Distance distance) => distance.Value;
}
