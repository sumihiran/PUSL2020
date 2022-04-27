using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class Institution
{
    public Institution()
    {
        Id = InstitutionId.New();
    }

    public InstitutionId Id { get; set; }
    public InstitutionType InstitutionType { get; set; }
    public string Name { get; set; }
    public Address? Address { get; set; }
    public string? PhoneNumber { get; set; }
}