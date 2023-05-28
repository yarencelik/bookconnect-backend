namespace BookConnect.Application.Features.Posts.Models;

public class PostsDetailsDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required string CreatedBy { get; set; }    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}