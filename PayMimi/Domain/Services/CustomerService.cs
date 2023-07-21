using FluentValidation;
using PayMimi.Infra.Http.Clients;

namespace PayMimi.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly ISocialNumberClient _client;
    private readonly IValidator<RegistrationIntentCommand> _validator; 

    public CustomerService(ILogger<CustomerService> logger, ISocialNumberClient client, IValidator<RegistrationIntentCommand> validator)
    {
        _logger = logger;
        _client = client;
        _validator = validator;
    }

    public async Task CreateRegistrationIntent(RegistrationIntentCommand command)
    {
        _logger.LogInformation("Creating registration intent for customer {Email}", command.Email);
        
        var result = await _validator.ValidateAsync(command);
        
        if (!result.IsValid)
        {
            _logger.LogError("Registration Validation errors: {Errors}", result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
            throw new InvalidOperationException("Registration Validation errors");
        }

        var response = await _client.IsValid(command.SocialNumber);

        _logger.LogInformation("Received response from social number validation service {Response}", response);

        if (!response.Valid) throw new Exception("Social number is invalid");

        _logger.LogInformation("Registration intent created for customer {Email}", command.Email);
    }
}