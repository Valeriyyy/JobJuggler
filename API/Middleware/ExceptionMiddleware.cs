using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {

        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError("Logging message in exception middlware");
            _logger.LogError(e.Message + " " + e.StackTrace);
            _logger.LogError($"Exception Type: {e.GetType().Name}");

            int statusCode = (int)HttpStatusCode.InternalServerError;
            // initialize a default problem details object
            var problem = new ProblemDetails
            {
                Status = statusCode,
                Type = "Server error",
                Title = "Server error",
                Detail = e.Message,
                Instance = "https://www.youtube.com/watch?v=H3EbflpXVmo"
            };


            if (e.GetType() == typeof(RecordNotFoundException))
            {
                statusCode = (int)HttpStatusCode.NotFound;
                problem.Status = statusCode;
                problem.Type = "RecordNotFound";
                problem.Title = "Record Not Found";
                problem.Detail = e.Message;
                problem.Instance = "https://www.youtube.com/watch?v=g3iFJpGJiug&t=31s";
            }

            if (e.GetType() == typeof(ValidationException))
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                problem.Status = statusCode;
                problem.Type = "BadRequest";
                problem.Title = "Invalid Request Body";
                problem.Detail = e.Message;
                problem.Instance = "https://www.youtube.com/watch?v=mWMFTfaJaWM";
            }

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problem.Status ?? statusCode;

            await context.Response.WriteAsync(json);
        }
    }
}
