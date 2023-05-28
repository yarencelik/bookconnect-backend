using AutoMapper;
using BookConnect.Application.Common.Models;
using BookConnect.Application.Features.Users;
using BookConnect.Application.Features.Users.Models;
using MediatR;

namespace BookConnect.Application.Features.Follow.Queries.GetUsersFollowers;

sealed class GetUsersFollowersQueryHandler : IRequestHandler<GetUsersFollowersQuery, PaginatedResults<UserDetailsDto>>
{
    private readonly IUsersRepository _usersRepository; 
    private readonly IMapper _mapper;
    public GetUsersFollowersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
       _usersRepository = usersRepository;
       _mapper = mapper; 
    }
    public async Task<PaginatedResults<UserDetailsDto>> Handle(GetUsersFollowersQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _usersRepository.GetUserFollowersOrFollowings(
            request.UserId,
            request.Page, 
            request.PageSize,
            true
            );

        var mappedResults = _mapper.Map<IEnumerable<UserDetailsDto>>(results);

        return new PaginatedResults<UserDetailsDto>(mappedResults, pageData);
    }
}