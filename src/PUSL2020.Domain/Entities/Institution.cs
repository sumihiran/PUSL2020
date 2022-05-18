using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class Institution
{
    public InstitutionId Id { get; set; }
    public InstitutionType InstitutionType { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}