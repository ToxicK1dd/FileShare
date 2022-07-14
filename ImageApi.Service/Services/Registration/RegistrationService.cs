using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Registration.Interface;
using MapsterMapper;

namespace ImageApi.Service.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegistrationService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}