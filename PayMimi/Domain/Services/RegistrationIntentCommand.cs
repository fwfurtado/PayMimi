namespace PayMimi.Domain.Services;

public class RegistrationIntentCommand
{
    public string Email { get; set; }

    public string SocialNumber { get; set; }
}