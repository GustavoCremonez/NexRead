using FluentValidation;
using NexRead.Dto.Author.Request;

namespace NexRead.Application.Validators;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage("Author id is required.");

        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must contain a minimum of 3 characters.")
            .MaximumLength(100).WithMessage("Name must contain a maximum of 100 characters.")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Author name cannot be empty or whitespace.");
    }
}
