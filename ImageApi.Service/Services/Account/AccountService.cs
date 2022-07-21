using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Account.Interface;
using MapsterMapper;

namespace ImageApi.Service.Services.Account
{
    /// <summary>
    /// Methods for managing accounts.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}