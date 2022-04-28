using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Employees;

public abstract class Employee
{
    public Employee()
    {
        Id = EmployeeId.New();
    }

    public EmployeeId Id { get; set; }
  
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string PasswordHash { get; set; }
}