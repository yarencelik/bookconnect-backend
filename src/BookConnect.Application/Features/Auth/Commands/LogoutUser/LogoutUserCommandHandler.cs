using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Features.Users;
using MediatR;

namespace BookConnect.Application.Features.Auth.Commands.LogoutUser;

sealed class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersRepository _usersRepository;
    public LogoutUserCommandHandler(IUsersRepository usersRepository, ICurrentUserService currentUserService)
    {
        _usersRepository = usersRepository;
        _currentUserService = currentUserService;
    }
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
            ?? throw new UnauthorizedAccessException();

        // For removing the refresh token in the cache.
        /*await _cache.RemoveAsync("PREV - " + user.Id.ToString(), cancellationToken);
        await _cache.RemoveAsync("NEW - " + user.Id.ToString(), cancellationToken);*/
    }
}
