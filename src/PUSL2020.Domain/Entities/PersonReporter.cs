using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class PersonReporter : Reporter
{
    public Nic? Nic { get; set; }
    
    public DriverLicenseId? DriverLicenseId { get; set; }
}