using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Admin
{
    public class Admin : User.User
    {
        /// <summary>
        /// The level of power the admin has
        /// </summary>
        public AdminRoleType RoleType { get; set; }
    }

    public class AdminEntityTypeConfiguration : BaseEntityTypeConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.RoleType)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(128);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.Admin)
                .HasForeignKey<Admin>(x => x.AccountId);
        }
    }

    public enum AdminRoleType
    {
        Global = 1,
        Support = 2
    }
}