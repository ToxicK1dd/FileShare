using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FileShare.Utilities.Helpers
{
    /// <summary>
    /// Static helper methods for claims.
    /// </summary>
    public static class IdentityClaimsHelper
    {
        /// <summary>
        /// Static helper method to get account id from HTTP context.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>The id of the account if found. Otherwise Guid.Empty.</returns>
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