using System.Linq.Expressions;
using PUSL2020.Application.Data;
using PUSL2020.Application.Dtos;
using PUSL2020.Application.Services;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.Entities.Employees;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Services;

public class AccidentService : IAccidentService
{
    private readonly IAccidentRepository _accidentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IClock _clock;
    private readonly IUnitOfWork _unitOfWork;

    public AccidentService(IAccidentRepository accidentRepository, IEmployeeRepository employeeRepository, IClock clock,
        IUnitOfWork unitOfWork)
    {
        _accidentRepository = accidentRepository;
        _employeeRepository = employeeRepository;
        _clock = clock;
        _unitOfWork = unitOfWork;
    }

    public async Task<Accident?> GetByRefIdAsync(RefId rid)
    {
        return await _accidentRepository.FindByIdAsync(rid);
    }

    public async Task UpdateAsync(Accident accident)
    {
        await _accidentRepository.UpdateAsync(accident);
    }

    public async Task DeleteAsync(Accident accident)
    {
        await _accidentRepository.DeleteAsync(accident);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ArchiveAsync(Accident accident)
    {
        accident.Archived = _clock.Now;
        await _unitOfWork.CompleteAsync();
    }

    public async Task ApproveByEmployeeIdAsync(Accident accident, EmployeeId eid)
    {
        var employee = await _employeeRepository.FindByIdAsync(eid);
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        SetApprovalStatusAndReasonByEmployee(accident, employee, status: true, null);
        await _unitOfWork.CompleteAsync();
    }

    public async Task RejectByEmployeeIdAsync(Accident accident, EmployeeId eid, string reason)
    {
        var employee = await _employeeRepository.FindByIdAsync(eid);
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        
        SetApprovalStatusAndReasonByEmployee(accident, employee, status: false, reason);
        await _unitOfWork.CompleteAsync();
    }

    private void SetApprovalStatusAndReasonByEmployee(Accident accident, Employee employee, bool status, string? reason)
    {
        switch (employee)
        {
            case RdaEmployee rdaOfficer:
                accident.RdaApproval = accident.RdaApproval ?? new();
                accident.RdaApproval.IsApproved = status;
                accident.RdaApproval.Reason = reason;
                accident.RdaApproval.Employee = rdaOfficer;
                break;
            case PoliceOfficer policeOfficer:
                accident.PoliceApproval = accident.PoliceApproval ?? new();
                accident.PoliceApproval.IsApproved = status;
                accident.PoliceApproval.Reason = reason;
                accident.PoliceApproval.Employee = policeOfficer;
                break;
            case InsuranceEmployee insuranceEmployee:
                accident.InsuranceApproval = accident.InsuranceApproval ?? new();
                accident.InsuranceApproval.IsApproved = status;
                accident.InsuranceApproval.Reason = reason;
                accident.InsuranceApproval.Employee = insuranceEmployee;
                break;
        }
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