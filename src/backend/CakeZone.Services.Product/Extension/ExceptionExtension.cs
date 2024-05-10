using CakeZone.Services.Product.Model.Error;
using CakeZone.Services.Product.Model.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CakeZone.Services.Product.Extension
{
    public static class ExceptionExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundApiException => StatusCodes.Status404NotFound,
                            BadRequestApiException => StatusCodes.Status400BadRequest,
                            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                            MutedApiException => StatusCodes.Status500InternalServerError,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            SattusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
}
