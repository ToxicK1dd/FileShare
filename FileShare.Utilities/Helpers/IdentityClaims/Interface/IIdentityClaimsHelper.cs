using Microsoft.AspNetCore.Http;

namespace FileShare.Utilities.Helpers.IdentityClaims.Interface
{
    public interface IIdentityClaimsHelper
    {
        Guid GetUserIdFromHttpContext(HttpContext httpContext);
    }
}