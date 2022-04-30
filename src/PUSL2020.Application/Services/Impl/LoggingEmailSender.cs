using Microsoft.Extensions.Logging;

namespace PUSL2020.Application.Services.Impl;

public class LoggingEmailSender : IEmailSender
{
    private readonly ILogger<LoggingEmailSender> _logger;

    public LoggingEmailSender(ILogger<LoggingEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogInformation(@"
Email: ${Email}\n
Subject: ${Subject},
Message:\n
${Message}
", email, subject, htmlMessage);
        return Task.CompletedTask;
    }
}
