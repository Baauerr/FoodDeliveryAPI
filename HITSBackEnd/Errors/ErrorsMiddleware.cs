using HITSBackEnd.Swagger;
using Newtonsoft.Json;

namespace HITSBackEnd.Errors
{
    public class ErrorsMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = StatusCodes.Status500InternalServerError;

            if (exception is BadRequestException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is NotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
            }

            var errorResponse = new ErrorResponseModel
            {
                status = statusCode,
                message = exception.Message
            };

            var errorResponseJson = JsonConvert.SerializeObject(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorResponseJson);
        }
    }
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorsMiddleware>();
        }
    }
}

