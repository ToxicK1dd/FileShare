using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Admin
{
    public class Admin : BaseEntity
    {
        /// <summary>
        /// The level of power the admin has
        /// </summary>
        public AdminRoleType RoleType { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }
        #endregion
    }

    public class AdminEntityTypeConfiguration : BaseEntityTypeConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.RoleType)
                .IsRequired();

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