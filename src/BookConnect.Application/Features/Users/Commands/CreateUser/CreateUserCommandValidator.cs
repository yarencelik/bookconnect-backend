using FluentValidation;

namespace BookConnect.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> 
{
    public CreateUserCommandValidator ()
    {
        
        RuleFor(x => x.Username)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .MinimumLength(4)
            .MaximumLength(12)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.FirstName)
            .MinimumLength(4)
            .MaximumLength(20);

        
        RuleFor(x => x.LastName)
            .MinimumLength(4)
            .MaximumLength(20);

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid User Role")
            .NotEmpty();
    }
}

