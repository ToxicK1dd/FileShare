namespace ImageApi.DataAccess.Models.Primary.Document
{
    public class Document : BaseEntity
    {
        /// <summary>
        /// User foreign key
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public User.User User { get; set; }

        /// <summary>
        /// Document bytes
        /// </summary>
        public byte[] Blob { get; set; }

        /// <summary>
        /// Document format, eg .png
        /// </summary>
        public string Format { get; set; }
    }
}