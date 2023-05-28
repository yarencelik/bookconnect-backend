namespace BookConnect.Application.Features.Posts.Models;

public sealed class CreatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}
