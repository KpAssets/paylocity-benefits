namespace Api.Extensions;

internal static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionHandlerMiddleware>();
}