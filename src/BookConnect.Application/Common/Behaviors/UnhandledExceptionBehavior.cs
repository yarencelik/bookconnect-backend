using MediatR;
using Microsoft.Extensions.Logging;

namespace BookConnect.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
       _logger = logger; 
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
           return await next(); 
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Error on Request {Name}, {@Request}, {@DateTime}", requestName, request, DateTime.Now);
            throw;
        }
    }
}