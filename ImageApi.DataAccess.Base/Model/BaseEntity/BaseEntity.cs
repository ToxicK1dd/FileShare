namespace ImageApi.DataAccess.Base.Model.BaseEntity
{
    /// <summary>
    /// Base class for all database models
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The time of which the entity was created
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// The date of when the entity last was changed
        /// </summary>
        public DateTimeOffset Changed { get; set; }

        /// <summary>
        /// Has the entity been soft deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}