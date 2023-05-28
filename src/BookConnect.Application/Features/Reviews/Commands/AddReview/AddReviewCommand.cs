using MediatR;

namespace App.Application.Features.Reviews.Commands.AddReview;

public class AddReviewCommand: IRequest
{
    public int Rating { get; set; }
    public string? Description { get; set; }
    public required string Book_Id { get; set; }
}