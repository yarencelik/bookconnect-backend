using App.Application.Features.Author.Models;
using FluentValidation;

namespace App.Application.Features.Author.Commands.UpdateAuthor;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x => x.AuthorName)
            .MinimumLength(4)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull();
    }
}

