using Microsoft.AspNetCore.Http;

namespace FileShare.Utilities.Helpers.IdentityClaims.Interface
{
    public interface IIdentityClaimsHelper
    {
        string GetUsernameFromHttpContext(HttpContext httpContext);
    }
}