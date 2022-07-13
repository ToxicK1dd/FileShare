using Microsoft.EntityFrameworkCore;

namespace ImageApi.DataAccess.Models.Primary
{
    public class PrimaryContext : BaseContext<PrimaryContext>
    {
        public PrimaryContext() { }

        public PrimaryContext(DbContextOptions<PrimaryContext> options) : base(options) { }
    }
}