using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Registration.Interface;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FileShare.Service.Services.V2._0.Registration
{
    /// <summary>
    /// Methods for registering accounts.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<object> _passwordHasher;
        private readonly IMapper _mapper;

        public RegistrationService(IPrimaryUnitOfWork unitOfWork, IPasswordHasher<object> passwordHasher, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }


        public async Task<(Guid loginId, Guid accountId)> RegisterAsync(string username, string email, string password, CancellationToken cancellationToken)
        {
            var isEmailValid = IsValidEmailAddress(email);
            if (isEmailValid is not true)
                throw new ArgumentException("Email is not in a valid format.");

            var isUsernameTaken = await _unitOfWork.LoginRepository.ExistsFromUsernameAsync(username, cancellationToken);
            if (isUsernameTaken)
                throw new ArgumentException("Username is already being used.");

            var isEmailTaken = await _unitOfWork.EmailRepository.ExistsFromAddressAsync(email, cancellationToken);
            if (isEmailTaken)
                throw new ArgumentException("Email is already being used.");

            var accountId = Guid.NewGuid();
            var loginId = Guid.NewGuid();
            var hashedPassword = _passwordHasher.HashPassword(null, password);

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