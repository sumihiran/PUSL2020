using Microsoft.AspNetCore.Identity;
using PUSL2020.Domain.Entities;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Infrastructure.Identity;

public class StaffUser : IdentityUser<StaffMemberId>
{
    private StaffMemberId _id;
    
    public StaffUser()
    {
        _id = StaffMemberId.New();
    }

    public StaffUser(StaffMember staffMember) 
    {
        StaffMember = staffMember;
    }

    public StaffMember? StaffMember { get; set; }
    
    public override StaffMemberId Id => StaffMember?.Id ?? _id;

    public override string UserName
    {
        get => StaffMember.UserName;
        set => StaffMember.UserName = value;
    }

    public override string PasswordHash
    {
        get => StaffMember.PasswordHash;
        set => StaffMember.PasswordHash = value;
    }
}