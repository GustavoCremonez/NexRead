using FluentValidation;
using NexRead.Dto.UserLibrary.Request;

namespace NexRead.Application.Validators;

public class AddBookToLibraryValidator : AbstractValidator<AddBookToLibraryRequest>
{
    public AddBookToLibraryValidator()
    {
        RuleFor(x => x.BookId)
            .GreaterThan(0).WithMessage("BookId must be greater than 0.");

        RuleFor(x => x.Status)
            .InclusiveBetween(1, 3).WithMessage("Status must be a valid ReadingStatus (WantToRead = 1, Reading = 2, Read = 3).");
    }
}
