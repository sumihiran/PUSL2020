using PUSL2020.Domain.Entities.Institutions;

namespace PUSL2020.Domain.Entities.Vehicles;

public class VehicleInsurance
{
    public string PolicyId { get; set; }
    public DateOnly StartAt { get; set; }
    public DateOnly ExpiryAt  { get; set; }
    
    public Insurance Issuer  { get; set; }
}