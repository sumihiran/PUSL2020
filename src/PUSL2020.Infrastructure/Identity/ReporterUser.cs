using Microsoft.AspNetCore.Identity;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Identity;

public class ReporterUser : IdentityUser<ReporterId>
{
    private ReporterId _id;
    
    public ReporterUser()
    {
        _id = ReporterId.New();
    }

    public ReporterUser(Reporter reporter)
    {
        Reporter = reporter;
    }

    public Reporter? Reporter { get; set; }

    public override ReporterId Id => Reporter?.Id ?? _id;

    public override string PhoneNumber
    {
        get => Reporter.PhoneNumber;
        set => Reporter.PhoneNumber = value;
    }

    public override string Email
    {
        get => Reporter.Email;
        set => Reporter.Email = value;
    }

    public override string PasswordHash
    {
        get => Reporter.PasswordHash;
        set => Reporter.PasswordHash = value;
    }
}