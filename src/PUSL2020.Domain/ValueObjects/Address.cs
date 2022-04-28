using PUSL2020.Domain.Enums;

#pragma warning disable CS8618
namespace PUSL2020.Domain.ValueObjects;

public class Address
{
    public string Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public District District { get; set; }
    public int? ZipCode { get; set; }
}