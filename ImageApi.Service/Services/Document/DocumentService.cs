using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Document.Interface;
using MapsterMapper;

namespace ImageApi.Service.Services.Document
{
    /// <summary>
    /// Methods for saving, and retrieving documents.
    /// </summary>
    public class DocumentService : IDocumentService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}