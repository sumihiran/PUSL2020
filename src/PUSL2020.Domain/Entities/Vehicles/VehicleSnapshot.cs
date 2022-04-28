using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Vehicles;

public  class VehicleSnapshot: AbstractVehicle
{
    public RefId AccidentId { get; set; }
    public Accident Accident { get; set; }
}