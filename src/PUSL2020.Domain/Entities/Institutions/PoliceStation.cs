using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Institutions;

public class PoliceStation : Institution<PoliceOfficer>
{
    public string Area { get; set; }
    public string Division { get; set; }
    public string Province { get; set; }
    public string? PhoneNumber { get; set; }
}