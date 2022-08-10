namespace FileShare.DataAccess.Base.Model.Entity.Interface
{
    public interface IIndexable
    {
        /// <summary>
        /// Primary id
        /// </summary>
        public Guid Id { get; set; }
    }
}