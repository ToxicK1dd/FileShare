using FileShare.DataAccess.Base.Model;
using Microsoft.EntityFrameworkCore;
using CustomIdentityUser = FileShare.DataAccess.Models.Primary.User.User;

namespace FileShare.DataAccess.Models.Primary
{
    public class PrimaryContext : BaseIdentityDbContext<PrimaryContext, CustomIdentityUser>
    {
        public PrimaryContext(DbContextOptions<PrimaryContext> options) : base(options) { }

        public PrimaryContext() { }
    }
}