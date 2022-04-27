using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class VehicleOwner
{
    public Name Name { get; set; }
    public Address Address { get; set; }
    public string Phone { get; set; }
}