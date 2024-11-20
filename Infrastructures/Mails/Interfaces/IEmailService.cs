using onboarding_dotnet.Infrastructures.Mails.Classes;

namespace onboarding_dotnet.Infrastructures.Mails.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);

        Task SendUsingTemplate(EmailMetadata emailMetadata, EmailTemplateModel model);
    }
}