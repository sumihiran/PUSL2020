using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;


public abstract class Reporter
{
    public Reporter()
    {
        Id = ReporterId.New();
    }

    public ReporterId Id { get; set; }
    public ReporterType ReporterType { get; set; }
    
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
   
    public Address Address { get; set; }
    public string PasswordHash { get; set; }
    
    public List<Vehicle> Vehicles { get; set; }
}