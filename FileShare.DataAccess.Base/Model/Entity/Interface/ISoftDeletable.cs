namespace FileShare.DataAccess.Base.Model.Entity.Interface
{
    public interface ISoftDeletable
    {
        /// <summary>
        /// Has the entity been soft deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// When was the entity deleted
        /// </summary>
        public DateTimeOffset Deleted { get; set; }
    }
}