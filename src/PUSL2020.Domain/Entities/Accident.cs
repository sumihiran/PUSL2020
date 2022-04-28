using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class Accident
{
    public Accident()
    {
        Id = RefId.New();
        RdaApproval = new();
        PoliceApproval = new();
        InsuranceApproval = new();
        Photos = new();
    }

    public RefId Id { get; set; }
    public Reporter Reporter { get; set; }
    public Driver Driver { get; set; }
    public DateTime DateTime { get; set; }
    public Location Location { get; set; }
    public AccidentCause Cause { get; set; }
    public string Reason { get; set; } 
    public string Description { get; set; } = "";

    public DateTime Reported { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Archived { get; set; }
    
    public VehicleSnapshot Vehicle { get; set; }
    
    public Approval<RdaEmployee> RdaApproval { get; set; }
    public Approval<PoliceOfficer> PoliceApproval { get; set; }
    public Approval<InsuranceEmployee> InsuranceApproval { get; set; }
    
    public List<ImageResource> Photos { get; set; }
}