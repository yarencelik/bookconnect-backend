using MediatR;

namespace BookConnect.Application.Features.Follow.Commands.UnfollowUser;

public record UnfollowUserCommand(string UserToFollowId) : IRequest;

