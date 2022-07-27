using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.Document.Document;

namespace FileShare.DataAccess.Repository.Primary.Document.Interface
{
    public interface IDocumentRepository : IRepositoryBase<Model>
    {
        Task<Model> GetByIdWithDetailAsync(Guid id, CancellationToken cancellationToken = default);
    }
}