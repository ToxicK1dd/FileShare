using FileShare.DataAccess.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace FileShare.Api.Setup
{
    public static class IdentitySetup
    {
        /// <summary>
        /// Configure Identity
        /// </summary>
        /// <param name="services"></param>
        public static void SetupIdentity(this IServiceCollection services)
        {
             services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }
    }
}