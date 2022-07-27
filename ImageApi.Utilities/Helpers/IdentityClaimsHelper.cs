using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ImageApi.Utilities.Helpers
{
    public static class IdentityClaimsHelper
    {
        public static Guid GetAccountIdFromHttpContext(this HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated is false)
                return Guid.Empty;

            if (httpContext.User.Identity is not ClaimsIdentity identity)
                return Guid.Empty;

            var accountId = identity.FindFirst("AccountId")?.Value;
            if (accountId is null)
                return Guid.Empty;

            return Guid.Parse(accountId);
        }
    }
}