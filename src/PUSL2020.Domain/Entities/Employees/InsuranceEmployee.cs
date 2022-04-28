using PUSL2020.Domain.Entities.Institutions;

namespace PUSL2020.Domain.Entities.Employees;

public class InsuranceEmployee : Employee
{
    public Insurance HeadOffice { get; set; }
}