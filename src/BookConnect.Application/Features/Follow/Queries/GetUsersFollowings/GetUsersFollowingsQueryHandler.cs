using App.Application.Common.Models;
using App.Application.Features.Users;
using App.Application.Features.Users.Models;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Follow.Queries.GetUsersFollowings;

sealed class GetUsersFollowingsQueryHandler : IRequestHandler<GetUsersFollowingsQuery, PaginatedResults<UserDetailsDto>>
{
    private readonly IUsersRepository _usersRepository; 
    private readonly IMapper _mapper;
    public GetUsersFollowingsQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
       _usersRepository = usersRepository;
       _mapper = mapper; 
    }
    public async Task<PaginatedResults<UserDetailsDto>> Handle(GetUsersFollowingsQuery request, CancellationToken cancellationToken)
    {
        var (results, pageData) = await _usersRepository.GetUserFollowersOrFollowings(
            request.UserId,
            request.Page, 
            request.PageSize,
            false 
            );

        var mappedResults = _mapper.Map<IEnumerable<UserDetailsDto>>(results);

        return new PaginatedResults<UserDetailsDto>(mappedResults, pageData);
    }
}