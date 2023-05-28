using System.Reflection;
using BookConnect.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace BookConnect.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    public AuthorizationBehavior(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizedAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if(authorizedAttributes.Any())
        {
            if(_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }
        }
        return await next();
    }
}