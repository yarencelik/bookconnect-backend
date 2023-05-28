using BookConnect.Application.Features.Posts.Models;
using FluentValidation;

namespace BookConnect.Application.Features.Posts.Commands.UpdatePost;

public class UpdatePostValidator : AbstractValidator<UpdatePostDto>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.Title)                
            .MinimumLength(8)
            .MaximumLength(20)
            .NotEmpty()
            .NotEmpty();

        RuleFor(x => x.Body)
            .MinimumLength(8)
            .MaximumLength(250)
            .NotEmpty()
            .NotNull();
    }
}