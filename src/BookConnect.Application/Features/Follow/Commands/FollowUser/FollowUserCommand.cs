using MediatR;

namespace BookConnect.Application.Features.Follow.Commands.FollowUser;

public record FollowUserCommand(string UserToFollowId ) : IRequest;

