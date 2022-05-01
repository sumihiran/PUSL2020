using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Entities.Institutions;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.Enums;

namespace PUSL2020.Tests.Shared;

using Bogus;

public static class FakeModel
{
    public static Faker<PersonReporter> PersonReporter => new Faker<PersonReporter>()
        .RuleFor(b => b.Nic, _ => FakeValueObject.OldNic())
        .RuleFor(b => b.Email, f => f.Person.Email)
        .RuleFor(b => b.Name, FakeValueObject.Name)
        .RuleFor(b => b.Address, FakeValueObject.Address)
        .RuleFor(b => b.PhoneNumber, f => f.Person.Phone);

    
    public static Faker<Insurance> Insurance => new Faker<Insurance>()
        .RuleFor(i => i.Name, f => f.Company.CompanyName())
        .RuleFor(i => i.PhoneNumber, f => f.Person.Phone);

    public static Faker<Accident> Accident => new Faker<Accident>()
        .RuleFor(a => a.Cause, f => f.PickRandom<AccidentCause>())
        .RuleFor(a => a.Reason, f=> f.Lorem.Sentence())
        .RuleFor(a => a.Vehicle, VehicleSnapshot)
        .RuleFor(a => a.DateTime, f => f.Date.Past())
        .RuleFor(a => a.Reported, f => f.Date.Recent())
        .RuleFor(a => a.Updated, f => f.Date.Soon())
        .RuleFor(a => a.Location, _ => FakeValueObject.Location);

    public static Faker<RdaEmployee> RdaEmployee => new Faker<RdaEmployee>()
        .RuleFor(a => a.UserName, f => f.Person.UserName)
        .RuleFor(a => a.DisplayName, f => f.Person.FullName);
    
    public static Faker<PoliceOfficer> PoliceOfficer => new Faker<PoliceOfficer>()
        .RuleFor(a => a.UserName, f => f.Person.UserName)
        .RuleFor(a => a.DisplayName, f => f.Person.FullName);
    
    public static Faker<InsuranceEmployee> InsuranceEmployee => new Faker<InsuranceEmployee>()
        .RuleFor(a => a.UserName, f => f.Person.UserName)
        .RuleFor(a => a.DisplayName, f => f.Person.FullName);
    
    public static Faker<VehicleSnapshot> VehicleSnapshot => new Faker<VehicleSnapshot>()
        .RuleFor(v => v.Make, f=> f.Vehicle.Manufacturer())
        .RuleFor(v => v.EngineNo, f=> f.Vehicle.Vin())
        .RuleFor(v => v.Class, f => f.PickRandom<VehicleClass>())
        .RuleFor(v => v.FuelType, f => f.PickRandom<FuelType>())
        .RuleFor(v => v.Model, f => f.Vehicle.Model())
        .RuleFor(v => v.Vrn, f => f.Random.String(10))
        .RuleFor(v => v.RegisteredAt, f => f.Date.PastDateOnly(10))
        .RuleFor(v => v.Owner, _ => FakeValueObject.VehicleOwner);

    public static Faker<Vehicle> Vehicle => new Faker<Vehicle>()
        .RuleFor(v => v.Make, f=> f.Vehicle.Manufacturer())
        .RuleFor(v => v.EngineNo, f=> f.Vehicle.Vin())
        .RuleFor(v => v.Class, f => f.PickRandom<VehicleClass>())
        .RuleFor(v => v.FuelType, f => f.PickRandom<FuelType>())
        .RuleFor(v => v.Model, f => f.Vehicle.Model())
        .RuleFor(v => v.Vrn, f => f.Random.String(10))
        .RuleFor(v => v.RegisteredAt, f => f.Date.PastDateOnly(10))
        .RuleFor(v => v.Owner, _ => FakeValueObject.VehicleOwner);
}