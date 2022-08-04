using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileShare.Api.Attributes
{
    /// <summary>
    /// Ensures the user has the required claim.
    /// </summary>
    public class RequireVerifiedAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                       .Any(x => x.GetType() == typeof(AllowAnonymousAttribute));

            if (isAllowAnonymous is false)
            {
                var userId = context.HttpContext.RequestServices.GetService<IIdentityClaimsHelper>().GetUserIdFromHttpContext(context.HttpContext);
                var verified = await context.HttpContext.RequestServices.GetService<IPrimaryUnitOfWork>().UserRepository.IsVerifiedByIdAsync(userId, context.HttpContext.RequestAborted);
                if (verified is false)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}