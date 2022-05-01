using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Data;
using PUSL2020.Application.Dtos;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Data.Repositories;

public class AccidentRepository : BaseRepository<Accident, RefId>, IAccidentRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AccidentRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<Dictionary<AccidentCause, long>> GetAccidentsCountGroupByCauseAsync(
        Expression<Func<Accident, bool>> predicate)
    {
        return _dbContext.Accidents
            .Where(predicate)
            .GroupBy(a => a.Cause)
            .Select(g => new { g.Key, Value = g.LongCount() })
            .ToDictionaryAsync(r => r.Key, r => r.Value);
    }

    public async Task<IEnumerable<VehicleAccidentCountByClassAndYearDto>> GetAccidentsPercentageGroupByVehicleClassAndYearAsync()
    {
        return await (from a in _dbContext.Accidents
            group a by new { a.Vehicle.Class, a.DateTime.Year }
            into grp
            select new VehicleAccidentCountByClassAndYearDto(grp.Key.Class, grp.Key.Year, grp.LongCount())
            ).ToListAsync();
    }
}