using FluentValidation;

namespace BookConnect.Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
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