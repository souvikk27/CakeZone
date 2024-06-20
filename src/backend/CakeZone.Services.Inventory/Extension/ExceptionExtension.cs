using CakeZone.Services.Inventory.Model.Error;
using CakeZone.Services.Inventory.Model.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CakeZone.Services.Inventory.Extension
{
    public static class ExceptionExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
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
                        var statusType = HttpStatus.GetHttpStatusType(context.Response.StatusCode);
                        logger.LogError($"Something went wrong {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            ApiResponseId = Guid.NewGuid(),
                            Instance = context.Request.Path,
                            StatusCode = context.Response.StatusCode,
                            Status = statusType.ToString(),
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
