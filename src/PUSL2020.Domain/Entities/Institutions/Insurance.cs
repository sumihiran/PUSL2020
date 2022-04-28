using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Institutions;

public class Insurance : Institution<InsuranceEmployee>
{
    public string Name { get; set; }
    public Address? Address { get; set; }
    public string? PhoneNumber { get; set; }
}