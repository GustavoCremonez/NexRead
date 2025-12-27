using FluentValidation;
using NexRead.Dto.Genre.Request;

namespace NexRead.Application.Validators;

public class CreateGenreValidator : AbstractValidator<CreateGenreRequest>
{
    public CreateGenreValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must contain a minimum of 3 characters.")
            .MaximumLength(255).WithMessage("Name must contain a maximum of 255 characters.")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Genre name cannot be empty or whitespace.");
    }
}
