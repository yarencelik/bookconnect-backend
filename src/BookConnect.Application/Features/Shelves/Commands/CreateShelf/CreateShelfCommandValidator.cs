using FluentValidation;

namespace BookConnect.Application.Features.Shelves.Commands.CreateShelf;

public class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
{
    public CreateShelfCommandValidator()
    {
        RuleFor(x => x.ShelfName)   
            .MinimumLength(4)
            .MaximumLength(24)
            .NotEmpty()
            .NotNull();
    } 
}