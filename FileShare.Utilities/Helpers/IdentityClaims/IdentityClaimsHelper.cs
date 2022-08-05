using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FileShare.Utilities.Helpers.IdentityClaims
{
    /// <summary>
    /// Static helper methods for claims.
    /// </summary>
    public class IdentityClaimsHelper : IIdentityClaimsHelper
    {
        /// <summary>
        /// Static helper method to get account id from HTTP context.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>The id of the account if found. Otherwise Guid.Empty.</returns>
        public string GetUsernameFromHttpContext(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated is false)
                return null;

            if (httpContext.User.Identity is not ClaimsIdentity identity)
                return null;

            var username = identity.FindFirst(ClaimTypes.Name)?.Value;
            if (username is null)
                return null;

            return username;
        }
    }
}