using Chronos.ApiResponse;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CakeZone.Services.Inventory.Services.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = ApiResponseExtension.ToErrorApiResult(context.ModelState);
            }
        }
    }
}
