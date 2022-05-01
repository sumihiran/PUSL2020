using Microsoft.EntityFrameworkCore;
using PUSL2020.Application.Data;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IReporterRepository _reporterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IVehicleRepository vehicleRepository, IReporterRepository reporterRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _reporterRepository = reporterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(Vid vid)
    {
        return await _vehicleRepository.FindByIdAsync(vid);
    }

    public async Task<Vehicle?> GetVehicleByReporterIdAndVrnAsync(ReporterId reporterId, string vrn)
    {
        var vehicles = await _vehicleRepository.FindByCondition(v => v.Reporter.Id == reporterId && v.Vrn == vrn)
            .ToListAsync();
        return vehicles.FirstOrDefault();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByReporterIdAsync(ReporterId reporterId)
    {
        return await _vehicleRepository.FindByCondition(v => v.Reporter.Id == reporterId).ToListAsync();
    }

    public async Task<bool> AddVehicleByReporterIdAsync(ReporterId reporterId, Vehicle vehicle)
    {
        var reporter = await _reporterRepository.FindByIdAsync(reporterId);
        if (reporter == null)
        {
            return false;
        }

        vehicle.Reporter = reporter;
        await _vehicleRepository.AddAsync(vehicle);

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public Task UpdateVehicleAsync(Vehicle vehicle)
    {
        return _vehicleRepository.UpdateAsync(vehicle);
    }

    public async Task<bool> DeleteById(Vid vid)
    {
        var vehicle = await _vehicleRepository.FindByIdAsync(vid);
        if (vehicle == null)
        {
            return false;
        }

        await _vehicleRepository.DeleteAsync(vehicle);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}