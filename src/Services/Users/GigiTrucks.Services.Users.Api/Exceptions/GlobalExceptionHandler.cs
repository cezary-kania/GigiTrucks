using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GigiTrucks.Services.Users.Api.Exceptions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Application error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An error occurred while processing your request."
        };
        
        Log.Error("Application error occured: {@Message}.", exception.Message);
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}