using PUSL2020.Application.Data;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Repositories;

public class EmployeeRepository : BaseRepository<Employee, EmployeeId>, IEmployeeRepository
{
    public EmployeeRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public override Task AddAsync(Employee entity)
    {
        throw new NotSupportedException("Use UserManager<EmployeeUser> instead.");
    }

    public override Task DeleteAsync(Employee entity)
    {
        throw new NotSupportedException("Use UserManager<EmployeeUser> instead.");
    }

    public override Task UpdateAsync(Employee entity)
    {
        throw new NotSupportedException("Use UserManager<EmployeeUser> instead.");
    }
}