using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class Vehicle
{
    public VehicleId Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Class { get; set; }
    public FuelType FuelType { get; set; }
    
    public string EngineNo { get; set; }
    public string Vrn { get; set; }
    public DateOnly RegisteredAt { get; set; }
    public Reporter Reporter { get; set; }
    
    public VehicleOwner Owner { get; set; }
    public VehicleInsurance Insurance { get; set; }
}