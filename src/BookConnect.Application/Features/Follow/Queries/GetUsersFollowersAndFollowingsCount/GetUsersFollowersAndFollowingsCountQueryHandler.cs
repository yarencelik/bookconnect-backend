using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Follow.Models;
using BookConnect.Application.Features.Users;
using MediatR;

namespace BookConnect.Application.Features.Follow.Queries.GetUsersFollowersAndFollowingsCount;

sealed class GetFollowsCountByUserIdQueryHandler : IRequestHandler<GetUsersFollowersAndFollowingsCountQuery, FollowersAndFollowingsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IFollowRepository _followRepository;
    public GetFollowsCountByUserIdQueryHandler (IFollowRepository followRepository, IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
        _followRepository = followRepository;  
    }
    public async Task<FollowersAndFollowingsDto> Handle(GetUsersFollowersAndFollowingsCountQuery request, CancellationToken cancellationToken)
    {
        var _ = await _usersRepository.GetValue(x => x.Id.ToString() == request.userId)
            ?? throw new NotFoundException($"User with Id '{request.userId}' was not found.");

        var followsCount = await _followRepository.GetUsersFollowsCount(request.userId);

        return followsCount;
    }
}
