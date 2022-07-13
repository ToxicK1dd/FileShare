using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.DocumentSignature.Interface;
using Model = ImageApi.DataAccess.Models.Primary.DocumentSignature.DocumentSignature;

namespace ImageApi.DataAccess.Repository.Primary.DocumentSignature
{
    public class DocumentSignatureRepository : RepositoryBase<Model, PrimaryContext>, IDocumentSignatureRepository
    {
        public DocumentSignatureRepository(PrimaryContext context) : base(context) { }
    }
}