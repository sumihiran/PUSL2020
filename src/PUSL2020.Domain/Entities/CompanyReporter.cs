using MultiScheme.Domain;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class CompanyReporter : Reporter 
{
    public Crn Crn { get; set; }
    public string LegalName { get; set; }
}