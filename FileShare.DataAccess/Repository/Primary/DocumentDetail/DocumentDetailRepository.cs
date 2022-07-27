using FileShare.DataAccess.Base.Repository;
using FileShare.DataAccess.Models.Primary;
using FileShare.DataAccess.Repository.Primary.DocumentDetail.Interface;
using Model = FileShare.DataAccess.Models.Primary.DocumentDetail.DocumentDetail;

namespace FileShare.DataAccess.Repository.Primary.DocumentDetail
{
    public class DocumentDetailRepository : RepositoryBase<Model, PrimaryContext>, IDocumentDetailRepository
    {
        public DocumentDetailRepository(PrimaryContext context) : base(context) { }
    }
}