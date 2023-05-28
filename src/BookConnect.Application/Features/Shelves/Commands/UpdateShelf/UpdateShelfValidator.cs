using BookConnect.Application.Features.Shelves.Models;
using FluentValidation;

namespace BookConnect.Application.Features.Shelves.Commands.UpdateShelf;

public sealed class UpdateShelfValidator : AbstractValidator<UpdateShelfDto>
{
    public UpdateShelfValidator()
    {
         RuleFor(x => x.ShelfName)   
            .MinimumLength(4)
            .MaximumLength(24)
            .NotEmpty()
            .NotNull();
    }
}