using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data;

public interface IVehicleRepository : IGenericRepository<Vehicle, Vid>
{
    
}