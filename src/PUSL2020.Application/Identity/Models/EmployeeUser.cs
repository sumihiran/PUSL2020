using Microsoft.AspNetCore.Identity;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Identity.Models;

public class EmployeeUser : IdentityUser<EmployeeId>
{
    private EmployeeId _id;
    
    public EmployeeUser()
    {
        _id = EmployeeId.New();
    }

    public EmployeeUser(Employee employee)
    {
        _id = employee.Id;
        Employee = employee;
    }

    public Employee? Employee { get; set; }
    
    public override EmployeeId Id => Employee?.Id ?? _id;

    public override string UserName
    {
        get => Employee.UserName;
        set => Employee.UserName = value;
    }

    public override string PasswordHash
    {
        get => Employee.PasswordHash;
        set => Employee.PasswordHash = value;
    }
}