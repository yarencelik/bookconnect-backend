using FluentValidation;

namespace App.Application.Features.Auth.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
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
    }
}