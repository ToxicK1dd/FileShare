using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.Document.Interface;
using Microsoft.EntityFrameworkCore;
using Model = FileShare.DataAccess.Models.Primary.Document.Document;

namespace FileShare.DataAccess.Repository.Primary.Document
{
    public class DocumentRepository : RepositoryBase<Model, PrimaryContext>, IDocumentRepository
    {
        public DocumentRepository(PrimaryContext context) : base(context) { }


        public async Task<Model> GetByIdWithDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(x => x.Id == id).Include(x => x.Detail).FirstOrDefaultAsync(cancellationToken);
        }
    }
}