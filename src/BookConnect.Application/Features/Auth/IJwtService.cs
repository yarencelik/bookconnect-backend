using BookConnect.Domain.Enums;

namespace BookConnect.Application.Features.Auth;

public interface IJwtService
{
    string GenerateJwt(Guid Id, UserRole role, bool isRefreshToken);
    bool ValidateRefreshToken(string oldToken, out string userId);
    Task<object?> ValidateRefreshToken(string oldToken);
    Task VerifyRefreshTokenInCache(string oldToken, string userId, CancellationToken cancellationToken);
}