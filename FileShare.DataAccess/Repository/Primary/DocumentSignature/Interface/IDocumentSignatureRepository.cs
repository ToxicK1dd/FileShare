using FileShare.DataAccess.Base.Repository.Interface;
using Model = FileShare.DataAccess.Models.Primary.DocumentSignature.DocumentSignature;

namespace FileShare.DataAccess.Repository.Primary.DocumentSignature.Interface
{
    public interface IDocumentSignatureRepository : IRepositoryBase<Model> { }
}