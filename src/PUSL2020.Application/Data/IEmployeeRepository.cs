using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data;

public interface IEmployeeRepository : IGenericRepository<Employee, EmployeeId>
{
    
}