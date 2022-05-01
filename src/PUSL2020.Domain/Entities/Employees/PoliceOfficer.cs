using PUSL2020.Domain.Entities.Institutions;

namespace PUSL2020.Domain.Entities.Employees;

public class PoliceOfficer : Employee
{
    public bool IsCaptain  { get; set; }
    public PoliceStation Station { get; set; }
}