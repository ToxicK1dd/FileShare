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
        public Guid GetUserIdFromHttpContext(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated is false)
                return Guid.Empty;

            if (httpContext.User.Identity is not ClaimsIdentity identity)
                return Guid.Empty;

            var userId = identity.FindFirst("UserId")?.Value;
            if (userId is null)
                return Guid.Empty;

            return Guid.Parse(userId);
        }
    }
}