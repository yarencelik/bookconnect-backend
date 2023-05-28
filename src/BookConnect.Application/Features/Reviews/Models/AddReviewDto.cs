namespace App.Application.Features.Reviews.Models;

public class AddReviewDto
{
    public int Rating { get; set; }
    public string? Description { get; set; }
}