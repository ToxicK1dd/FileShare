using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Share.Interface;
using MapsterMapper;

namespace ImageApi.Service.Services.Share
{
    /// <summary>
    /// Methods for creating, and managing sharing of documents.
    /// </summary>
    public class ShareService : IShareService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShareService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}