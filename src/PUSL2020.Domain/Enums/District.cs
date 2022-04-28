using System.Runtime.Serialization;

namespace PUSL2020.Domain.Enums;

public enum District
{
    Colombo,
    Gampaha,
    Kalutara,
    Kandy,
    Matale,
    [EnumMember(Value = "Nuwara Eliya")] 
    NuwaraEliya,
    Galle,
    Matara,
    Hambantota,
    Jaffna,
    Kilinochchi,
    Mannar,
    Vavuniya,
    Mullaitivu,
    Batticaloa,
    Ampara,
    Trincomalee,
    Kurunegala,
    Puttalam,
    Anuradhapura,
    Polonnaruwa,
    Badulla,
    Moneragala,
    Ratnapura,
    Kegalle
}