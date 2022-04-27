using Bogus;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Tests.Shared;

public static class FakeValueObject
{
    private static Faker Instance => new();

    public static Faker<Name> Name => new Faker<Name>()
        .RuleFor(b => b.First, f => f.Person.FirstName)
        .RuleFor(b => b.Middle, f => f.Person.FirstName)
        .RuleFor(b => b.Last, f => f.Person.LastName);

    public static Faker<Address> Address => new Faker<Address>()
        .RuleFor(b => b.Line1, f => f.Address.BuildingNumber())
        .RuleFor(b => b.Street, f => f.Address.StreetName())
        .RuleFor(b => b.City, f => f.Address.City())
        .RuleFor(b => b.District, f => f.Address.State())
        .RuleFor(b => b.ZipCode, f => f.Random.Int(10000, 99000));


    public static Nic OldNic()
    {
        var birthYear = Instance.Random.Int(40, 99).ToString();
        var dayOfYear = Instance.Random.Int(1, 365).ToString().PadLeft(3, '0');
        var serial = Instance.Random.Int(0, 999).ToString().PadLeft(3, '0');
        var checkDigit = Instance.Random.Int(0, 9);
        return new Nic($"{birthYear}{dayOfYear}{serial}{checkDigit}V");
    }

    public static Faker<VehicleInsurance> VehicleInsurance => new Faker<VehicleInsurance>()
        .RuleFor(b => b.PolicyId, f => f.Finance.Account(8))
        .RuleFor(b => b.StartAt, f => f.Date.RecentDateOnly())
        .RuleFor(b => b.ExpiryAt, f => f.Date.FutureDateOnly(5));
}