using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileShare.Filters
{
    public class RequireEnabledFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                    .Any(x => x.GetType() == typeof(AllowAnonymousAttribute));

            if (isAllowAnonymous is false)
            {
                var accountId = context.HttpContext.GetAccountIdFromHttpContext();
                var enabled = await context.HttpContext.RequestServices.GetService<IPrimaryUnitOfWork>().AccountRepository.IsEnabledByIdAsync(accountId, context.HttpContext.RequestAborted);
                if (enabled is false)
                    context.Result = new UnauthorizedResult();
            }

            await next();
        }
    }
}