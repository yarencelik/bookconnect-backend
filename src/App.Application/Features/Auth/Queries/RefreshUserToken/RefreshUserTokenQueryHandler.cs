using App.Application.Features.Auth;
using App.Application.Features.Auth.Models;
using App.Application.Features.Auth.Queries.RefreshUserToken;
using App.Application.Features.Users;
using AutoMapper;
using MediatR;

sealed class RefreshUserTokenQueryHandler : IRequestHandler<RefreshUserTokenQuery, (AuthDetailsDto, string)>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public RefreshUserTokenQueryHandler(IUsersRepository usersRepository, IMapper mapper, IJwtService jwtService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<(AuthDetailsDto, string)> Handle(RefreshUserTokenQuery request, CancellationToken cancellationToken)
    {
        var isTokenValid = _jwtService.ValidateRefreshToken(request.OldToken, out string userId);

        if(!isTokenValid)
            throw new UnauthorizedAccessException("Invalid Token");

        // Will throw error if Refresh Token was invalid in cache.
        // await _tokenService.VerifyRefreshTokenInCache(request.OldToken, userId, cancellationToken);

        var user = await _usersRepository.GetValue(x => x.Id.ToString() == userId) 
            ?? throw new UnauthorizedAccessException("User not found.");


        var mapped = _mapper.Map<AuthDetailsDto>(user);
        mapped.AccessToken = _jwtService.GenerateJwt(user.Id, user.Role, false);
        string refreshToken = _jwtService.GenerateJwt(user.Id, user.Role, true);

        // For saving the refresh token in the cache.
        /*await _cache.SetStringAsync("PREV - " + user.Id.ToString(), request.OldToken, cancellationToken);
        await _cache.SetStringAsync("NEW - " + user.Id.ToString(), refreshToken, cancellationToken);*/

        return (mapped, refreshToken);
    }
}