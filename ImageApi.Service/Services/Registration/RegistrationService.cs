using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Services.Registration.Interface;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ImageApi.Service.Services.Registration
{
    /// <summary>
    /// Methods for registering accounts.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegistrationService(IPrimaryUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<(Guid loginId, Guid accountId)> Register(string username, string email, string password, CancellationToken cancellationToken)
        {
            var isEmailValid = IsValidEmailAddress(email);
            if (isEmailValid is not true)
                throw new Exception("Email is not in a valid format.");

            var isUsernameTaken = await _unitOfWork.LoginRepository.ExistsFromUsernameAsync(username, cancellationToken);
            if (isUsernameTaken)
                throw new Exception("Username is already being used.");

            var isEmailTaken = await _unitOfWork.EmailRepository.ExistsFromAddress(email, cancellationToken);
            if (isEmailTaken)
                throw new Exception("Email is already being used.");

            var accountId = Guid.NewGuid();
            var loginId = Guid.NewGuid();
            var hashedPassword = new PasswordHasher<object>().HashPassword(null, password);

            var account = new DataAccess.Models.Primary.Account.Account()
            {
                Id = accountId,
                Enabled = true,
                Verified = false,
                Login = new()
                {
                    Id = loginId,
                    AccountId = accountId,
                    Username = username,
                    Password = hashedPassword
                },
                Email = new()
                {
                    Address = email,
                }
            };
            await _unitOfWork.AccountRepository.AddAsync(account, cancellationToken);

            return (loginId, accountId);
        }


        #region Helpers
        public static bool IsValidEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return new EmailAddressAttribute().IsValid(email);
        }
        #endregion
    }
}