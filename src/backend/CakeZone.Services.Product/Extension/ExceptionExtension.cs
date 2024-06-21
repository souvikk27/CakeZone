using CakeZone.Services.Product.Model.Error;
using CakeZone.Services.Product.Services.Logging;
using Microsoft.AspNetCore.Diagnostics;
using BadRequestApiException = CakeZone.Services.Product.Model.Exception.BadRequestApiException;
using ErrorDetails = CakeZone.Services.Product.Model.Error.ErrorDetails;
using MutedApiException = CakeZone.Services.Product.Model.Exception.MutedApiException;
using NotFoundApiException = CakeZone.Services.Product.Model.Exception.NotFoundApiException;

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

