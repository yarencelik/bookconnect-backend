using AutoMapper;
using BookConnect.Application.Common.Exceptions;
using BookConnect.Application.Features.Users.Models;
using MediatR;

namespace BookConnect.Application.Features.Users.Queries;

public record GetUserByIdQuery(string? userId) : IRequest<UserDetailsDto>;

sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;   
        _mapper = mapper;
    }
    public async Task<UserDetailsDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == request.userId);

        if(user == null)
        {
            throw new NotFoundException($"User with Id: {request.userId} was not found");
        }

        var mappedUser = _mapper.Map<UserDetailsDto>(user);

        return mappedUser;
    }
}
