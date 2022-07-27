using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Account.Interface;
using MapsterMapper;

namespace FileShare.Service.Services.V2._0.Account
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