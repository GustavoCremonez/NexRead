using FluentValidation;
using NexRead.Dto.Auth.Request;

namespace NexRead.Application.Validators;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.")
            .Must(token => !string.IsNullOrWhiteSpace(token)).WithMessage("Refresh token cannot be empty or whitespace.");
    }
}
