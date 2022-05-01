using PUSL2020.Application.Dtos;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Services;

public interface IAccidentService
{
    Task<Accident?> GetByRefIdAsync(RefId rid);
    Task UpdateAsync(Accident accident);
    Task DeleteAsync(Accident accident);
    Task ArchiveAsync(Accident accident);

    Task ApproveByEmployeeIdAsync(Accident accident, EmployeeId eid);
    
    Task RejectByEmployeeIdAsync(Accident accident, EmployeeId eid, string reason);

    Task<Dictionary<AccidentCause, float>> GetAccidentsPercentageByVehicleClassGroupByCauseAsync(
        VehicleClass vehicleClass);

    Task<IEnumerable<VehicleAccidentPercentageByClassAndYearDto>> GetAccidentsPercentageGroupByVehicleClassAndYearAsync();
}