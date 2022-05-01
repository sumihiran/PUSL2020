using PUSL2020.Application.Dtos;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Services;

public interface IAccidentService
{
    Task<Accident?> GetByRefIdAsync(RefId rid);
    Task<bool> UpdateAsync(Accident accident);
    Task<bool> DeleteByIdAsync(RefId rid);
    Task<bool> ArchiveByIdAsync(RefId rid);

    Task<Dictionary<AccidentCause, float>> GetAccidentsPercentageByVehicleClassGroupByCauseAsync(
        VehicleClass vehicleClass);

    Task<IEnumerable<VehicleAccidentPercentageByClassAndYearDto>> GetAccidentsPercentageGroupByVehicleClassAndYearAsync();
}