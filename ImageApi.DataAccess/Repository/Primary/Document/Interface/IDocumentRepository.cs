using ImageApi.DataAccess.Base.Repository.Interface;
using Model = ImageApi.DataAccess.Models.Primary.Document.Document;

namespace ImageApi.DataAccess.Repository.Primary.Document.Interface
{
    public interface IDocumentRepository : IRepositoryBase<Model> { }
}