using MediatR;

namespace App.Application.Features.Posts.Commands.DeletePost;

public record DeletePostCommand(string postId) : IRequest;