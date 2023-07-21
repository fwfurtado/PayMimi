using FluentValidation;
using PayMimi.Web.Requests;

namespace PayMimi.Web.Validations;

public class CustomerRegistrationIntentValidator : AbstractValidator<CustomerRegistrationIntentRequest>
{
    public CustomerRegistrationIntentValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.SocialNumber)
            .NotEmpty()
            .Matches(@"^\d{3}.\d{3}.\d{3}-\d{2}$");
    }
}