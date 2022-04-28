using PUSL2020.Domain.Enums;

namespace PUSL2020.Domain.ValueObjects;

public class Location
{
    public string Road { get; set;  }
    public District District { get; set;  }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}