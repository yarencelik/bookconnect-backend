using FluentValidation;

namespace App.Application.Features.Author.Commands.AddAuthor;

public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorName)
            .MinimumLength(4)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull();
    }
}

