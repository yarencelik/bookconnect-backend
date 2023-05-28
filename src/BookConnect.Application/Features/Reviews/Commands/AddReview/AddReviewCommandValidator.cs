using FluentValidation;

namespace BookConnect.Application.Features.Reviews.Commands.AddReview;

public sealed class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
{
    public AddReviewCommandValidator()
    {
        RuleFor(x => x.Rating)
            .GreaterThan(1)
            .LessThanOrEqualTo(5)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Description)
            .MaximumLength(150);
    }
}