using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.Document.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Document.Document;

namespace ImageApi.DataAccess.Repository.Primary.Document
{
    public class DocumentRepository : RepositoryBase<Model, PrimaryContext>, IDocumentRepository
    {
        public DocumentRepository(PrimaryContext context) : base(context) { }
    }
}