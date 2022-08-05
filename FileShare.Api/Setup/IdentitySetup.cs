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
            services.AddIdentity<DataAccess.Models.Primary.User.User, IdentityRole<Guid>>()
               .AddEntityFrameworkStores<PrimaryContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.  
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.  
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.  
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#";
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}