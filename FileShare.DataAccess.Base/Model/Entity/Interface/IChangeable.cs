namespace FileShare.DataAccess.Base.Model.Entity.Interface
{
    public interface IChangeable
    {
        /// <summary>
        /// The date of when the entity last was changed
        /// </summary>
        public DateTimeOffset Changed { get; set; }
    }
}