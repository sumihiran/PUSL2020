using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Services;

public interface IVehicleService
{
    Task<Vehicle?> GetVehicleByIdAsync(Vid vid);
    Task<Vehicle?> GetVehicleByReporterIdAndVrnAsync(ReporterId reporterId, string vrn);
    Task<IEnumerable<Vehicle>> GetVehiclesByReporterIdAsync(ReporterId reporterId);
    Task<bool> AddVehicleByReporterIdAsync(ReporterId reporterId, Vehicle vehicle);
    Task UpdateVehicleAsync(Vehicle vehicle);
    Task<bool> DeleteById(Vid vid);
}