namespace PUSL2020.Domain.Entities;

public class VehicleInsurance
{
    public string PolicyId { get; set; }
    public DateOnly StartAt { get; set; }
    public DateOnly ExpiryAt  { get; set; }
    
    public Insurance Issuer  { get; set; }
}