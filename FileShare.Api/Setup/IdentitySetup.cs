using FileShare.DataAccess.Models.Primary;
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
             services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<PrimaryContext>()
                .AddDefaultTokenProviders();
        }
    }
}