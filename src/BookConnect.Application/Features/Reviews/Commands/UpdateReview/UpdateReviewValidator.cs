using App.Application.Features.Reviews.Models;
using FluentValidation;

namespace App.Application.Features.Reviews.Commands.UpdateReview;

public sealed class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewValidator()
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