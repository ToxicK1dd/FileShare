using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageApi.DataAccess.Models.Primary.User
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Full name of the user
        /// </summary>
        public string FullName { get; set; }


        #region Navigation Properties
        /// <summary>
        /// Account foreign key
        /// </summary>
        public Guid? AccountId { get; set; }

        /// <summary>
        /// Account navigation property
        /// </summary>
        public Account.Account Account { get; set; }

        /// <summary>
        /// Navigation property for documents
        /// </summary>
        public ICollection<Document.Document> Documents { get; set; } 
        #endregion
    }

    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FullName)
                .HasMaxLength(128);

            builder.HasOne(x => x.Account)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.AccountId);
        }
    }
}