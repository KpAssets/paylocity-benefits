using System.Text.Json;
using Api.Models;
using Business.Exceptions;

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
            await WriteApiResponseForException(
                context,
                StatusCodes.Status400BadRequest,
                "Input validations failed",
                ex);
        }
        catch (NotFoundException ex)
        {
            await WriteApiResponseForException(
                context,
                StatusCodes.Status404NotFound,
                "Requested resource not found",
                ex);
        }
        catch (Exception ex)
        {
            await WriteApiResponseForException(
                context,
                StatusCodes.Status500InternalServerError,
                "Unexpected exception occurred",
                ex);
        }
    }

    private async Task WriteApiResponseForException(
        HttpContext context,
        int statusCode,
        string message,
        Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

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