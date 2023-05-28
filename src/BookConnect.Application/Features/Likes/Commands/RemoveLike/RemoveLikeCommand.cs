using MediatR;

namespace BookConnect.Application.Features.Likes.Commands.RemoveLike;

public record RemoveLikeCommand(string postId) : IRequest;