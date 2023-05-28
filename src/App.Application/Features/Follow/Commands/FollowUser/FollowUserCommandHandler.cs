using App.Application.Common.Exceptions;
using App.Application.Common.Interfaces;
using App.Application.Features.Users;
using FollowEntity = App.Domain.Entities.Follow;
using MediatR;

namespace App.Application.Features.Follow.Commands.FollowUser;

sealed class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IFollowRepository _followRepository;
    public FollowUserCommandHandler(IUsersRepository usersRepository, IFollowRepository followRepository, ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _followRepository = followRepository;
        _currentUserService = currentUserService;
    }
    public async Task Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId) 
            ?? throw new UnauthorizedAccessException();

        if(_currentUserService.UserId == request.UserToFollowId)
            throw new ConflictException("User cannot follow his/her own.");

        var userToFollow = await _usersRepository.GetValue(x => x.Id.ToString() == request.UserToFollowId, null, false)
            ?? throw new NotFoundException("User was not found");

        var follow = await _followRepository.GetValue(
            x => x.Follower_Id.ToString() == user.Id.ToString() &&
            x.Following_Id.ToString() == request.UserToFollowId);

        if (follow != null)
            throw new ConflictException($"User already Followed this User with Id '{request.UserToFollowId}'");

        var newFollow = new FollowEntity
        {
            Follower_Id = user.Id,
            Following_Id = userToFollow.Id
        };

        await _followRepository.Create(newFollow);
        await _followRepository.SaveChangesAsync();
    }
}
