using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data;

public interface IAccidentRepository : IGenericRepository<Accident, RefId>
{
    
}