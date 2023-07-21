namespace PayMimi.Domain.Services;

public interface ICustomerService
{
    Task CreateRegistrationIntent(RegistrationIntentCommand command);
}