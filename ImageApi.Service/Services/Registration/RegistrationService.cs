﻿using ImageApi.DataAccess.Models.Primary.Account;
using ImageApi.DataAccess.UnitOfWork.Primary.Interface;
using ImageApi.Service.Dto.Registration;
using ImageApi.Service.Services.Registration.Interface;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
            var isEmailValid = IsValidEmailAddress(dto.Email);
            if (isEmailValid is not true)
                throw new Exception("Email is not in a valid format.");

            var isUsernameTaken = await _unitOfWork.LoginRepository.ExistsFromUsernameAsync(dto.Username, cancellationToken);
            if (isUsernameTaken)
                throw new Exception("Username is already being used.");

            var isEmailTaken = await _unitOfWork.ContactInfoRepository.ExistsFromEmail(dto.Email, cancellationToken);
            if (isEmailTaken)
                throw new Exception("Email is already being used.");

            var accountId = Guid.NewGuid();
            var loginId = Guid.NewGuid();
            var hashedPassword = new PasswordHasher<object>().HashPassword(null, dto.Password);

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
                },
                AccountInfo = new()
                {
                    ContactInfo = new()
                    {
                        Email = dto.Email,
                    }
                }
            };
            await _unitOfWork.AccountRepository.AddAsync(account, cancellationToken);

            return (loginId, accountId);
        }


        #region Helpers
        public bool IsValidEmailAddress(string email)
        {
            if (!string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
        #endregion
    }
}