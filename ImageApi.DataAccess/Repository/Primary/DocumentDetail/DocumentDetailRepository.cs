using ImageApi.DataAccess.Base.Repository;
using ImageApi.DataAccess.Models.Primary;
using ImageApi.DataAccess.Repository.Primary.DocumentDetail.Interface;
using Model = ImageApi.DataAccess.Models.Primary.DocumentDetail.DocumentDetail;

namespace ImageApi.DataAccess.Repository.Primary.DocumentDetail
{
    public class DocumentDetailRepository : RepositoryBase<Model, PrimaryContext>, IDocumentDetailRepository
    {
        public DocumentDetailRepository(PrimaryContext context) : base(context) { }
    }
}