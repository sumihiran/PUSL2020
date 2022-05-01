using System.Runtime.Serialization;

namespace PUSL2020.Domain.Enums;

public enum AccidentCause
{
    [EnumMember(Value = "Bad Weather")]
    BadWeather,
    
    Distractions,
    Speeding,
    [EnumMember(Value = "Overtaking Error")]
    OvertakingError,
    [EnumMember(Value = "Drunk Driving")]
    DrunkDriving,
    [EnumMember(Value = "Technical Fault")]
    TechnicalFault,
    Other
}