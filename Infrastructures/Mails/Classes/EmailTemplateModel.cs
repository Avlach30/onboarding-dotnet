namespace onboarding_dotnet.Infrastructures.Mails.Classes;

public class EmailTemplateModel(
    string? name,
    string? email
)
{
    public string? Name { get; set; } = name;
    public string? Email { get; set; } = email;
}