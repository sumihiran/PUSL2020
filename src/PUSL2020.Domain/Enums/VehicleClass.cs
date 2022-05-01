using System.Runtime.Serialization;

namespace PUSL2020.Domain.Enums;

public enum VehicleClass
{
    [EnumMember(Value ="Motorcycle")]
    A,
    [EnumMember(Value ="Three Wheel")]
    B1,
    [EnumMember(Value ="Car")]
    B,
    [EnumMember(Value ="Lorry")]
    C,
    [EnumMember(Value ="Bus")]
    D,
    [EnumMember(Value ="Other")]
    G,
}