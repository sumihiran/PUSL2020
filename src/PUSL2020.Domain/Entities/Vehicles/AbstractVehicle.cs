using PUSL2020.Domain.Enums;

namespace PUSL2020.Domain.Entities.Vehicles;

public class AbstractVehicle
{
    public string Vrn { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public VehicleClass Class { get; set; }
    public FuelType FuelType { get; set; }
    public string EngineNo { get; set; }
    public DateOnly RegisteredAt { get; set; }
    public VehicleOwner Owner { get; set; }
    public VehicleInsurance Insurance { get; set; }

    public void SetOwnerFromReporter(Reporter reporter)
    {
        Owner = Owner ?? new VehicleOwner();
        Owner.Address = reporter.Address.Clone();
        Owner.Name = reporter switch
        {
            PersonReporter personReporter => personReporter.Name.ToString(),
            CompanyReporter companyReporter => companyReporter.LegalName,
            _ => throw new ArgumentOutOfRangeException(nameof(reporter), reporter, null)
        };
    }
}