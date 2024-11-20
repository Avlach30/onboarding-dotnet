using FluentEmail.Core;
using onboarding_dotnet.Infrastructures.Mails.Classes;
using onboarding_dotnet.Infrastructures.Mails.Interfaces;

namespace onboarding_dotnet.Infrastructures.Mails.Services;

public class EmailService(
    IFluentEmail fluentEmail,
    ILogger<EmailService> logger
): IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    private readonly ILogger<EmailService> _logger = logger;

    public async Task Send(EmailMetadata emailMetadata)
    {
        try
        {
            await _fluentEmail
                .To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                .SendAsync();

            _logger.LogInformation("Email sent to {ToAddress}", emailMetadata.ToAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {ToAddress}", emailMetadata.ToAddress);
            throw;
        }
    }

    public async Task SendUsingTemplate(EmailMetadata emailMetadata, EmailTemplateModel model)
    {
        try
        {
            await _fluentEmail
                .To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .UsingTemplate(emailMetadata.Body, model)
                .SendAsync();

            _logger.LogInformation("Email sent to {ToAddress}", emailMetadata.ToAddress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {ToAddress}", emailMetadata.ToAddress);
            throw;
        }
    }
}