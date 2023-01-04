using System.Text.Json;
using Api.Models;

namespace Api;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidDataException ex)
        {
            await WriteApiResponseForException(context, "Input validations failed", ex);
        }
        catch (Exception ex)
        {
            await WriteApiResponseForException(context, "Unexpected exception occurred", ex);
        }
    }

    private async Task WriteApiResponseForException(
        HttpContext context,
        string message,
        Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status200OK;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(
                new ApiResponse<object>
                {
                    Success = false,
                    Message = message,
                    Error = ex.Message
                }));
    }
}