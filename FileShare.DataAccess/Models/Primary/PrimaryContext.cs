using FileShare.DataAccess.Base.Model;
using Microsoft.EntityFrameworkCore;

namespace FileShare.DataAccess.Models.Primary
{
    public class PrimaryContext : BaseContext<PrimaryContext>
    {
        public PrimaryContext(DbContextOptions<PrimaryContext> options) : base(options) { }
        
        public PrimaryContext() { }
    }
}