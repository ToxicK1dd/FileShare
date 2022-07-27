using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.DocumentSignature.Interface;
using Model = FileShare.DataAccess.Models.Primary.DocumentSignature.DocumentSignature;

namespace FileShare.DataAccess.Repository.Primary.DocumentSignature
{
    public class DocumentSignatureRepository : RepositoryBase<Model, PrimaryContext>, IDocumentSignatureRepository
    {
        public DocumentSignatureRepository(PrimaryContext context) : base(context) { }
    }
}