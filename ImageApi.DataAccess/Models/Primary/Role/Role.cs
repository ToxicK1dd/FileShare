using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.Role
{
    public class Role : BaseEntity
    {
        /// <summary>
        /// Type of account role
        /// </summary>
        public RoleType Type { get; set; }


        #region Navigation Properties

        public ICollection<Account.Account> Accounts { get; set; }

        #endregion
    }

    public class RoleEntityTypeConfiguration : BaseEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasMany(x => x.Accounts)
                .WithMany(x => x.Roles);
        }
    }

    public enum RoleType
    {
        Admin = 1,
        Support = 2
    }
}