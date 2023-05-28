using App.Application.Common.Exceptions;
using App.Application.Features.Auth;
using App.Domain.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Services;
public sealed class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly IDistributedCache _cache;

    public JwtService(IConfiguration config, IDistributedCache cache)
    {
        _config = config;
        _cache = cache;
    }

    public string GenerateJwt(Guid Id, UserRole role, bool isRefreshToken)
    {
        var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Id.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var tokenToWrite = new JwtSecurityToken
            (
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                DateTime.Now,
                isRefreshToken ? DateTime.Now.AddDays(7) : DateTime.Now.AddMinutes(12),
                signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenToWrite);
    }

    public bool ValidateRefreshToken(string oldToken, out string userId)
    {
        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config["Authentication:Issuer"],
            ValidAudience = _config["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]!)
                )
        };
        var decodedToken = new JwtSecurityTokenHandler()
            .ValidateToken(oldToken, tokenValidationParams, out SecurityToken validatedToken);

        if (
            validatedToken is not JwtSecurityToken jwtSecurityToken
        || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
        ||  decodedToken.Identity == null 
        ||  decodedToken.Identity.Name == null
        )
        {
            userId = string.Empty; 
            return false;
        }

        userId = decodedToken.Identity.Name;
        return true; 
    }

    public Task<object?> ValidateRefreshToken(string oldToken)
    {
        throw new NotImplementedException();
    }

    public async Task VerifyRefreshTokenInCache(string oldToken, string userId, CancellationToken cancellationToken)
    {
        // Get Previous and Current Refresh token in the cache.
        var newToken = await _cache.GetStringAsync("NEW - " + userId, cancellationToken);
        var prevToken = await _cache.GetStringAsync("PREV - " + userId, cancellationToken);


        /* Check if user doesn't have the previous token or not equal to the current token
         * this verification is useful when the user/malicious user try to refresh 
         * their non-existing token in db  
         */
        if (string.IsNullOrEmpty(prevToken))
        {
            if (newToken != oldToken)
            {
                await _cache.RemoveAsync("NEW - " + userId, cancellationToken);
                await _cache.RemoveAsync("PREV - " + userId, cancellationToken);
                throw new Exception("The Refresh Token that was requested is not the same as the current token.");
            }
        }
        else
        {
            /*
             * Check if user's refresh token if it's not equal to the current refresh token,
             * and check if the user's refresh token is the same as the previous
             * and if token was not existed in the cache, it will throw error.
             */
            if (newToken != oldToken)
            {
                if (prevToken == oldToken)
                {
                    await _cache.RemoveAsync("NEW - " + userId, cancellationToken);
                    await _cache.RemoveAsync("PREV - " + userId, cancellationToken);
                    throw new Exception("The Refresh Token that was requested is the same as the previous/last token.");
                }
                else
                {
                    await _cache.RemoveAsync("NEW - " + userId, cancellationToken);
                    await _cache.RemoveAsync("PREV - " + userId, cancellationToken);
                    throw new NotFoundException("No refresh token found in the cache.");
                }
            }
        }
    }
}
