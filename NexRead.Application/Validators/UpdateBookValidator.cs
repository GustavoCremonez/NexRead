using FluentValidation;
using NexRead.Dto.Book.Request;

namespace NexRead.Application.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(500).WithMessage("Title must contain a maximum of 500 characters.");

        RuleFor(b => b.Description)
            .MaximumLength(5000).WithMessage("Description must contain a maximum of 5000 characters.")
            .When(b => !string.IsNullOrWhiteSpace(b.Description));

        RuleFor(b => b.ImageUrl)
            .MaximumLength(1000).WithMessage("ImageUrl must contain a maximum of 1000 characters.")
            .When(b => !string.IsNullOrWhiteSpace(b.ImageUrl));

        RuleFor(b => b.Language)
            .MaximumLength(10).WithMessage("Language must contain a maximum of 10 characters.")
            .When(b => !string.IsNullOrWhiteSpace(b.Language));

        RuleFor(b => b.PageCount)
            .GreaterThan(0).WithMessage("PageCount must be greater than 0.")
            .When(b => b.PageCount.HasValue);

        RuleFor(b => b.AverageRating)
            .InclusiveBetween(0, 5).WithMessage("AverageRating must be between 0 and 5.")
            .When(b => b.AverageRating.HasValue);

        RuleFor(b => b.AuthorIds)
            .NotEmpty().WithMessage("At least one author is required.");

        RuleFor(b => b.GenreIds)
            .NotEmpty().WithMessage("At least one genre is required.");
    }
}
