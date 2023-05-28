using FluentValidation;

namespace BookConnect.Application.Features.Books.Commands.AddBook;

public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    
    public AddBookCommandValidator()
    {
        RuleFor(x => x.ISBN)
            .Matches("(?=(?:\\D*\\d){10}(?:(?:\\D*\\d){3})?$)").WithMessage("Invalid ISBN")
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .MinimumLength(4)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Pages)
            .GreaterThan(10)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Title)
            .MinimumLength(1)
            .NotNull()
            .NotEmpty();
    }
}