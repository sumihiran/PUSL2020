using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;

namespace PUSL2020.Domain.ValueObjects;

public class Approval<T> where T: Employee
{
    public bool? IsApproved { get; set; }
    public string? Reason { get; set; }
    public T? Employee { get; set; }
}