using MediatR;

namespace App.Application.Features.Follow.Commands.UnfollowUser;

public record UnfollowUserCommand(string UserToFollowId) : IRequest;

