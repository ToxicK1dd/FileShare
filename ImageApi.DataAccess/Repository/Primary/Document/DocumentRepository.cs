using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using Microsoft.EntityFrameworkCore;
using Model = ImageApi.DataAccess.Models.Primary.Document.Document;

namespace ImageApi.DataAccess.Repository.Primary.Document
{
    public class DocumentRepository : RepositoryBase<Model, PrimaryContext>, IDocumentRepository
    {
        public DocumentRepository(PrimaryContext context) : base(context) { }


        public async Task<Model> GetByIdWithDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<Model>().Where(x => x.Id == id).Include(x => x.Detail).FirstOrDefaultAsync(cancellationToken);
        }
    }
}