using ImageApi.DataAccess.Models.Primary.Account;
using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Dto.Registration;
using ImageApi.Service.Services.Registration.Interface;
using MapsterMapper;
using Microsoft.AspNet.Identity;

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


        public async Task<(Guid loginId, Guid accountId)> Register(RegistrationDto dto, CancellationToken cancellationToken)
        {
            var accountId = Guid.NewGuid();
            var loginId = Guid.NewGuid();
            var hashedPassword = new PasswordHasher().HashPassword(dto.Password);

            var account = new DataAccess.Models.Primary.Account.Account()
            {
                Id = accountId,
                Enabled = true,
                Validated = false,
                Type = AccountType.User,
                Login = new()
                {
                    Id = loginId,
                    AccountId = accountId,
                    Username = dto.Username,
                    Password = hashedPassword
                }
            };
            await _unitOfWork.AccountRepository.AddAsync(account, cancellationToken);

            return (loginId, accountId);
        }
    }
}