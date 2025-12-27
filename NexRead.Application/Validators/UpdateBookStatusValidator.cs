using FluentValidation;
using NexRead.Dto.UserLibrary.Request;

namespace NexRead.Application.Validators;

public class UpdateBookStatusValidator : AbstractValidator<UpdateBookStatusRequest>
{
    public UpdateBookStatusValidator()
    {
        RuleFor(x => x.Status)
            .InclusiveBetween(1, 3).WithMessage("Status must be a valid ReadingStatus (WantToRead = 1, Reading = 2, Read = 3).");
    }
}
