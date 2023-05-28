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
    // TODO: Update Logging Behavior to check if authenticated User does exists.
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Requested {Name}, {@DateTime}", requestName, DateTime.Now);
        await Task.CompletedTask;
    }
}
