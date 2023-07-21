using FluentValidation;
using PayMimi.Domain.Repositories;
using PayMimi.Domain.Services;

namespace PayMimi.Domain.Validations;

public class RegistrationIntentValidator : AbstractValidator<RegistrationIntentCommand>
{
    private readonly ICustomerRepository _repository;

    public RegistrationIntentValidator(ICustomerRepository repository)
    {
        _repository = repository;

        SetupValidations();
    }

    private void SetupValidations()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid")
            .CustomAsync(async (email, ctx, cancellationToken) =>
            {
                var exists = await _repository.AlreadyExists(email, cancellationToken);
                if (exists) ctx.AddFailure("Email already exists");
            });
    }
}