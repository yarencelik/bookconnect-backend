using MediatR;

namespace App.Application.Features.Likes.Commands.RemoveLike;

public record RemoveLikeCommand(string postId) : IRequest;