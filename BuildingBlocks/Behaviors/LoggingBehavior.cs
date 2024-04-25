using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handling request={Request} - Response={Response} - RequestData={RequestData}", typeof(TRequest).Name,
            typeof(TResponse).Name, request);
        
        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] Long running request={Request} - TimeTaken={TimeTaken}", typeof(TRequest).Name,
                timeTaken.Seconds);
        }
        
        logger.LogInformation("[END] Handled request={Request} - Response={Response} - RequestData={RequestData}", typeof(TRequest).Name,
            typeof(TResponse).Name, request);
        return response;
    }
}