namespace FileShare.DataAccess.Base.Model.Entity.Interface
{
    public interface ICreatable
    {
        /// <summary>
        /// The time of which the entity was created
        /// </summary>
        public DateTimeOffset Created { get; set; }
    }
}