using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;

namespace PUSL2020.Tests.Shared;

using Bogus;

public static class FakeModel
{
    public static Faker<PersonReporter> PersonReporter => new Faker<PersonReporter>()
        .RuleFor(b => b.Nic, _ => FakeValueObject.OldNic())
        .RuleFor(b => b.Email, f => f.Person.Email)
        .RuleFor(b => b.Name, FakeValueObject.Name)
        .RuleFor(b => b.Address, FakeValueObject.Address);

    public static Faker<Institution> Institution(InstitutionType institutionType) => new Faker<Institution>()
        .RuleFor(i => i.InstitutionType, _ => institutionType)
        .RuleFor(i => i.Address, f => FakeValueObject.Address)
        .RuleFor(i => i.Name, f => f.Company.CompanyName())
        .RuleFor(i => i.PhoneNumber, f => f.Person.Phone);
    
    public static Faker<Insurance> Insurance => new Faker<Insurance>()
        .RuleFor(i => i.InstitutionType, _ => InstitutionType.Insurance)
        .RuleFor(i => i.Address, f => FakeValueObject.Address)
        .RuleFor(i => i.Name, f => f.Company.CompanyName())
        .RuleFor(i => i.PhoneNumber, f => f.Person.Phone);

    
    public static Faker<Vehicle> Vehicle => new Faker<Vehicle>()
        .RuleFor(v => v.Make, f=> f.Vehicle.Manufacturer())
        .RuleFor(v => v.EngineNo, f=> f.Vehicle.Vin())
        .RuleFor(v => v.Class, f => f.Vehicle.Type())
        .RuleFor(v => v.FuelType, f => f.PickRandom<FuelType>())
        .RuleFor(v => v.Model, f => f.Vehicle.Model())
        .RuleFor(v => v.Vrn, f => f.Random.String(10))
        .RuleFor(v => v.RegisteredAt, f => f.Date.PastDateOnly(10));
}