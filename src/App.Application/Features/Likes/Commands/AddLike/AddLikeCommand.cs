using MediatR;

namespace App.Application.Features.Likes.Commands.AddLike;

public record AddLikeCommand(string postId) : IRequest;