namespace FileShare.DataAccess.Base.Model.Entity.Interface
{
    public interface IRetrievable
    {
        /// <summary>
        /// When the entity was last retrieved
        /// </summary>
        public DateTimeOffset Retrieved { get; set; }
    }
}