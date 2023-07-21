using PayMimi.Infra.Http.Clients;

namespace PayMimi.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly ISocialNumberClient _client;

    public CustomerService(ILogger<CustomerService> logger, ISocialNumberClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task CreateRegistrationIntent(RegistrationIntentCommand command)
    {
        _logger.LogInformation("Creating registration intent for customer {Email}", command.Email);

        var response = await _client.IsValid(command.SocialNumber);

        _logger.LogInformation("Received response from social number validation service {Response}", response);

        if (!response.Valid) throw new Exception("Social number is invalid");

        _logger.LogInformation("Registration intent created for customer {Email}", command.Email);
    }
}