using CakeZone.Services.Product.Model.API;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Extension
{
    public static class ApiResponseExtension
    {
        private const string GenericSuccessMessage = "The operation completed succesfully.";
        private const string GenericWarningMessage = "The operation completed with warnings.";

        public static ObjectResult ToSuccessApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new ApiResponseModel<object>(
                    ApiResponseStatusEnum.Success,
                    message ?? GenericSuccessMessage,
                    payload,
                    statuscode));

        public static ObjectResult ToWarningApiResult(object payload, string message = null, string statuscode = null) =>
            new ObjectResult(
                new ApiResponseModel<object>(
                    ApiResponseStatusEnum.Warning,
                    message ?? GenericWarningMessage,
                    payload,
                    statuscode));


        public static ObjectResult ToInfoApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new ApiResponseModel<object>(
                    ApiResponseStatusEnum.Info,
                    message,
                    payload,
                    statuscode));

        public static ObjectResult ToErrorApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new ApiResponseModel<object>(
                    ApiResponseStatusEnum.Error,
                    message,
                    payload,
                    statuscode));
    }
}
