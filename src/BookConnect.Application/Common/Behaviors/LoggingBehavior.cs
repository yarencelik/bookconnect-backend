using MediatR.Pipeline;
using Microsoft.Extensions.Logging;


namespace BookConnect.Application.Common.Behaviors;
public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull 
{
    private readonly ILogger<LoggingBehavior<TRequest>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest>> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Requested {Name}, {@DateTime}", requestName, DateTime.Now);
        await Task.CompletedTask;
    }

    // public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    // {
    //     // TODO: Update Logging Behavior especially for ValidationException and other exceptions
    //     try
    //     {
    //         _logger.LogInformation("Completed request {@RequestName}, {@DateTime}",
    //             typeof(TRequest).Name,
    //             DateTime.UtcNow);

    //         var results = await next();

    //         _logger.LogInformation("Completed request {@RequestName}, {@DateTime}",
    //             typeof(TRequest).Name,
    //             DateTime.UtcNow);
    //         return results;
    //     }
    //     catch (Exception ex)
    //     {
    //         var requestName = typeof(TRequest).Name;
    //         if(ex is ValidationException validationException)
    //         {
    //             _logger.LogError(validationException, "VerticalSlice Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
    //         }

    //         _logger.LogError(ex, "VerticalSlice Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

    //         return await next();
    //     }
    // }
}
