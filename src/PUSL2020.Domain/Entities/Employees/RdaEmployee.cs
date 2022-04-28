using PUSL2020.Domain.Entities.Institutions;

namespace PUSL2020.Domain.Entities.Employees;

public class RdaEmployee : Employee
{
    public RdaOffice Office { get; set; }
}