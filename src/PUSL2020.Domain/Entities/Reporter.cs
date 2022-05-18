using PUSL2020.Domain.Enums;
using PUSL2020.Domain.ValueObjects;

namespace PUSL2020.Domain.Entities;

/// <summary>
/// Represents a user in the identity system
/// </summary>
public abstract class Reporter
{
    protected Reporter()
    {
        Id = ReporterId.New();
    }

    public ReporterId Id { get; set; }

    /// <summary>
    /// Gets or sets the type  for this reporter.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public ReporterType ReporterType { get; set; }

    /// <summary>
    /// Gets or sets the email address for this reporter.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a telephone number for the reporter.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the name for this reporter.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the name for this reporter.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets a salted and hashed representation of the password for this reporter.
    /// </summary>
    public string PasswordHash { get; set; }
}