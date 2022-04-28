using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Vehicles;

public class Vehicle : AbstractVehicle
{
    public Vehicle()
    {
        Id = Vid.New();
    }

    public Vid Id { get; set; }
    public Reporter Reporter { get; set; }
    
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}