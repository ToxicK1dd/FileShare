using FileShare.DataAccess.Base.Model.Entity.Interface;

namespace FileShare.DataAccess.Base.Model.Entity
{
    /// <summary>
    /// Base class for all database models
    /// </summary>
    public abstract class BaseEntity : ICreatable, IRetrievable, IChangeable, ISoftDeletable
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
        /// When the entity was last retrieved
        /// </summary>
        public DateTimeOffset Retrieved { get; set; }

        /// <summary>
        /// The date of when the entity last was changed
        /// </summary>
        public DateTimeOffset Changed { get; set; }

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