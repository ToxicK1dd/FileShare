using ImageApi.DataAccess.Base.Model.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.AccountRole
{
    public class AccountRole : BaseEntity
    {
        /// <summary>
        /// Type of account role
        /// </summary>
        public RoleType Type { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public ICollection<Account.Account> Accounts { get; set; }
        #endregion
    }

    public class AccountRoleEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountRole>
    {
        public override void Configure(EntityTypeBuilder<AccountRole> builder)
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