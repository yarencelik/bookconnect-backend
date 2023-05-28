using MediatR;

namespace App.Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Guid>
{
    public required string Title { get; set; }
    public required string Body { get; set; }
}

