using System.Linq.Expressions;
using PUSL2020.Application.Data;
using PUSL2020.Application.Dtos;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Services.Impl;

public class AccidentService : IAccidentService
{
    private readonly IAccidentRepository _accidentRepository;

    public AccidentService(IAccidentRepository accidentRepository)
    {
        _accidentRepository = accidentRepository;
    }

    public Task<Accident?> GetByRefIdAsync(RefId rid)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Accident accident)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(RefId rid)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ArchiveByIdAsync(RefId rid)
    {
        throw new NotImplementedException();
    }

    public async Task<Dictionary<AccidentCause, float>> GetAccidentsPercentageByVehicleClassGroupByCauseAsync(
        VehicleClass vehicleClass)
    {
        Expression<Func<Accident, bool>> vehiclesByClass = a => a.Vehicle.Class == vehicleClass;

        var total = await _accidentRepository.CountAsync(vehiclesByClass);
        var accidentsCountByCause = await _accidentRepository.GetAccidentsCountGroupByCauseAsync(vehiclesByClass);

        return accidentsCountByCause.ToDictionary(a => a.Key, item => (float)(item.Value * 100) / total);
    }

    public async Task<IEnumerable<VehicleAccidentPercentageByClassAndYearDto>>
        GetAccidentsPercentageGroupByVehicleClassAndYearAsync()
    {
        var accidentsCountByYear = (await _accidentRepository.GetAccidentsPercentageGroupByVehicleClassAndYearAsync())
            .GroupBy(a => a.Year)
            .ToDictionary(a => a.Key, a => a.ToList());

        var totalAccidentsByYear = accidentsCountByYear.ToDictionary(i => i.Key, i => i.Value.Sum(a => a.Quantity));

        return accidentsCountByYear
            .Select(group =>
            {
                return group.Value.Select(a =>
                {
                    var percentage = ((float)a.Quantity / totalAccidentsByYear[group.Key]) * 100;
                    return new VehicleAccidentPercentageByClassAndYearDto(a.VehicleClass, group.Key, percentage);
                });
            })
            .SelectMany(i => i);
    }
}