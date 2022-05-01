namespace PUSL2020.Application.Services;

public interface IClock
{
    DateTime Now { get; }
    
    DateTimeOffset UtcNow { get; }
}