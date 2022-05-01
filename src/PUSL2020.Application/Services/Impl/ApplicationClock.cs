using Microsoft.AspNetCore.Authentication;

namespace PUSL2020.Application.Services.Impl;

public class ApplicationClock : IClock, ISystemClock
{
    public DateTime Now => DateTime.Now;

    public DateTimeOffset UtcNow
    {
        get
        {
            var utcNowPrecisionSeconds = new DateTime((DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond) * TimeSpan.TicksPerSecond, DateTimeKind.Utc);
            return new DateTimeOffset(utcNowPrecisionSeconds);
        }
    }
}