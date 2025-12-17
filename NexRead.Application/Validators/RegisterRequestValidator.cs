using FluentValidation;
using NexRead.Dto.Auth.Request;

namespace NexRead.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must contain a minimum of 2 characters.")
            .MaximumLength(100).WithMessage("Name must contain a maximum of 100 characters.")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name cannot be empty or whitespace.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(255).WithMessage("Email must contain a maximum of 255 characters.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must contain a minimum of 8 characters.")
            .MaximumLength(40).WithMessage("Password must contain a maximum of 40 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
    }
}
