using MediatR;

namespace App.Application.Features.Follow.Commands.FollowUser;

public record FollowUserCommand(string UserToFollowId ) : IRequest;

