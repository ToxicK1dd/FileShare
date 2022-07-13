namespace ImageApi.DataAccess.Base.Repository.Interface
{
    /// <summary>
    /// Base repository for easy and fast development of repositories.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IRepositoryBase<TModel>
    {
        /// <summary>
        /// Add a <see cref="TModel"/> to the database.
        /// </summary>
        /// <param name="model"></param>
        Task AddAsync(
             TModel model,
             CancellationToken cancellationToken = default);

        /// <summary>
        /// Get an entity by the primary id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        Task<TModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="model"></param>
        void Update(TModel model);

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="model"></param>
        void Remove(TModel model);

        /// <summary>
        /// Check if an entity with the specified id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boolean value indicating the existance of an entity</returns>
        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}