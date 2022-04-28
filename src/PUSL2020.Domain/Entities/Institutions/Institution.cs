using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Institutions;

public abstract class Institution<TEmployee> where TEmployee: Employee
{
    public Institution()
    {
        Id = InstitutionId.New();
    }

    public InstitutionId Id { get; set; }
    
    public virtual List<TEmployee> Employees { get; set; }
}