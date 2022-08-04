using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileShare.Api.Filters
{
    public class RequireEnabledFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                    .Any(x => x.GetType() == typeof(AllowAnonymousAttribute));

            if (isAllowAnonymous is false)
            {
                var userId = context.HttpContext.RequestServices.GetService<IIdentityClaimsHelper>().GetUsernameFromHttpContext(context.HttpContext);
                var enabled = await context.HttpContext.RequestServices.GetService<IPrimaryUnitOfWork>().UserRepository.IsEnabledByUsernameAsync(userId, context.HttpContext.RequestAborted);
                if (enabled is false)
                    context.Result = new UnauthorizedResult();
            }

            await next();
        }
    }
}