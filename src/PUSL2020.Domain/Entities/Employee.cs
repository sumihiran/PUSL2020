using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class Employee
{
    public Employee()
    {
        Id = EmployeeId.New();
    }

    public EmployeeId Id { get; set; }
    
    public Institution Office { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string PasswordHash { get; set; }
}