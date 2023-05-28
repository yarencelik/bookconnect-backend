using System.Security.Claims;
using App.Application.Common.Interfaces;

namespace App.API.Service;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;
    public CurrentUserService(IHttpContextAccessor httpContext)
    {
       _httpContext = httpContext; 
    }
    public string? UserId => _httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
}