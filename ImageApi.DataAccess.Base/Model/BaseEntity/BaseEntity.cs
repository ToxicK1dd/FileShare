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

        ///// <summary>
        ///// The time of which the entity was created
        ///// </summary>
        //public DateTimeOffset Created { get; set; }

        ///// <summary>
        ///// The date of when the entity last was changed
        ///// </summary>
        //public DateTimeOffset Changed { get; set; }

        ///// <summary>
        ///// The account id of who last changed the entity
        ///// </summary>
        //public Guid ChangedBy { get; set; }

        ///// <summary>
        ///// The time of which the entity last was accessed
        ///// </summary>
        //public DateTimeOffset Accessed { get; set; }

        ///// <summary>
        ///// The account id of who last accssed the entity
        ///// </summary>
        //public Guid AccessedBy { get; set; }
    }
}