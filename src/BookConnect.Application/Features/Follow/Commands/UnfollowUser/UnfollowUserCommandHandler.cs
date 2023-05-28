using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Users;
using MediatR;

namespace BookConnect.Application.Features.Follow.Commands.UnfollowUser;

sealed class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    private readonly IFollowRepository _followRepository;
    public UnfollowUserCommandHandler(IUsersRepository usersRepository, IFollowRepository followRepository, ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _followRepository = followRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UnfollowUserCommand  request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
            ?? throw new UnauthorizedAccessException();

        if(_currentUserService.UserId == request.UserToFollowId)
            throw new ConflictException("User cannot unfollow his/her own.");

        var userToFollow = await _usersRepository.GetValue(x => x.Id.ToString() == request.UserToFollowId, null, false)
            ?? throw new NotFoundException("User was not found");

        var follow = await _followRepository.GetValue(
            x => x.Follower_Id.ToString() == user.Id.ToString() &&
            x.Following_Id.ToString() == request.UserToFollowId, null, false) 
            ?? throw new NotFoundException("Follow record was not found");

        _followRepository.Delete(follow);
        await _followRepository.SaveChangesAsync();
    }
}