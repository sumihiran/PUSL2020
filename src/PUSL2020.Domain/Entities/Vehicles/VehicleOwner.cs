using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities.Vehicles;

public class VehicleOwner
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public string Phone { get; set; }
}