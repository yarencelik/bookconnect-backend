namespace App.Application.Features.Reviews.Models;

public sealed class UpdateReviewDto
{
    public int Rating { get; set; }
    public string? Description { get; set; }
}