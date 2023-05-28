namespace App.Application.Features.Posts.Models;

public class UpdatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}