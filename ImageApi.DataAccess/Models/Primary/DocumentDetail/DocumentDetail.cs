namespace ImageApi.DataAccess.Models.Primary.DocumentDetail
{
    public class DocumentDetail : BaseEntity
    {
        /// <summary>
        /// Document foreign key
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Document navigation property
        /// </summary>
        public Document.Document Document { get; set; }

        /// <summary>
        /// The time of when the document was accessed
        /// </summary>
        public DateTimeOffset Accessed { get; set; }

        /// <summary>
        /// The user who accessed the document
        /// </summary>
        public Guid AccessedBy { get; set; }
    }
}