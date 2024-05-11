using Microsoft.AspNetCore.Mvc;
using ApiResponseStatusEnum = CakeZone.Services.Product.Model.Generic.ApiResponseStatusEnum;

namespace CakeZone.Services.Product.Extension
{
    public static class ApiResponseExtension
    {
        private const string GenericSuccessMessage = "The operation completed succesfully.";
        private const string GenericWarningMessage = "The operation completed with warnings.";

        public static ObjectResult ToSuccessApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new Model.Generic.ApiResponseModel<object>(
                    ApiResponseStatusEnum.Success,
                    message ?? GenericSuccessMessage,
                    payload,
                    statuscode));

        public static ObjectResult ToWarningApiResult(object payload, string message = null, string statuscode = null) =>
            new ObjectResult(
                new Model.Generic.ApiResponseModel<object>(
                    ApiResponseStatusEnum.Warning,
                    message ?? GenericWarningMessage,
                    payload,
                    statuscode));


        public static ObjectResult ToInfoApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new Model.Generic.ApiResponseModel<object>(
                    ApiResponseStatusEnum.Info,
                    message,
                    payload,
                    statuscode));

        public static ObjectResult ToErrorApiResult(object payload, string message = null, string statuscode = null) =>
            new OkObjectResult(
                new Model.Generic.ApiResponseModel<object>(
                    ApiResponseStatusEnum.Error,
                    message,
                    payload,
                    statuscode));
    }
}
