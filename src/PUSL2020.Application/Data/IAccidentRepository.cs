using System.Linq.Expressions;
using PUSL2020.Application.Dtos;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data;

public interface IAccidentRepository : IGenericRepository<Accident, RefId>
{
    Task<Dictionary<AccidentCause, long>> GetAccidentsCountGroupByCauseAsync(Expression<Func<Accident, bool>> predicate);

    Task<IEnumerable<VehicleAccidentCountByClassAndYearDto>> GetAccidentsPercentageGroupByVehicleClassAndYearAsync();
}