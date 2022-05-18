using Microsoft.AspNetCore.Identity;
using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

public class StaffMember
{
    public StaffMember()
    {
    }

    public StaffMember(Institution office)
    {
        Office = office;
    }

    public StaffMemberId Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    public virtual string UserName { get; set; }
    
    /// <summary>
    /// Gets or sets the name for this reporter.
    /// </summary>
    public string DisplayName { get; set; }
    
    /// <summary>
    /// Gets or sets a salted and hashed representation of the password for this reporter.
    /// </summary>
    public string PasswordHash { get; set; }
    
    public InstitutionType OfficeType { get; set; }
    
    public Institution Office { get; set; }
}