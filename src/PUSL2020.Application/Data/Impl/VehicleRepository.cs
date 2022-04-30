using PUSL2020.Domain.Entities.Vehicles;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Application.Data.Impl;

public class VehicleRepository : BaseRepository<Vehicle, Vid>, IVehicleRepository
{
    public VehicleRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }
}