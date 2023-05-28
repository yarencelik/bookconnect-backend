using MediatR;

namespace BookConnect.Application.Features.Posts.Commands.DeletePost;

public record DeletePostCommand(string postId) : IRequest;