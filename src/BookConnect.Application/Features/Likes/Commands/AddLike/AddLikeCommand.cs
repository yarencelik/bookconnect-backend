using MediatR;

namespace BookConnect.Application.Features.Likes.Commands.AddLike;

public record AddLikeCommand(string postId) : IRequest;