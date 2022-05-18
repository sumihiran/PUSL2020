using Microsoft.Extensions.Logging;
using PUSL2020.Application.Services;

namespace PUSL2020.Infrastructure.Services;

public class LoggingEmailSender : IEmailSender
{
    private readonly ILogger<LoggingEmailSender> _logger;

    public LoggingEmailSender(ILogger<LoggingEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogInformation("[email]: {email}\n [Subject]: {subject}\n[Message]\n{htmlMessage}", 
            email, subject, htmlMessage);
        
        return Task.CompletedTask;
    }
}