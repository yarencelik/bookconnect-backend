using AutoMapper;
using BookConnect.Application.Features.Auth.Models;
using BookConnect.Application.Features.Users;
using MediatR;

namespace BookConnect.Application.Features.Auth.Commands.LoginUser;

sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, (AuthDetailsDto, string)>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;
    public LoginUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IJwtService jwtService, IPasswordService passwordService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    public async Task<(AuthDetailsDto, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Username == request.Username);

        if(user == null || !_passwordService.VerifyPassword(user.Password, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid Username or Password.");
        }

        var mappedAuthDetails = _mapper.Map<AuthDetailsDto>(user);
        mappedAuthDetails.AccessToken = _jwtService.GenerateJwt(user.Id, user.Role, false);
        string refreshToken = _jwtService.GenerateJwt(user.Id, user.Role, true);

        // For saving the refresh token in cache
        // await _cache.SetStringAsync($"NEW - {user.Id}", refreshToken, cancellationToken);

        return (mappedAuthDetails, refreshToken);
    }
}