using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Institutions;

public class RdaOffice : Institution<Employee>
{
    public District District { get; set; }
    public string? PhoneNumber { get; set; }
}