using FluentValidation;
using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(5).WithMessage("The name is too short.")
            .MaximumLength(100).WithMessage("The name is too long.");
        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("The phone number should only contain numbers")
            .Length(7,15);
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.CityId).GreaterThan(0).WithMessage("Valid City ID is required");
    }
}