﻿using FileShare.DataAccess.Models.Primary.User;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.Token.Interface;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileShare.Service.Services.Token
{
    /// <summary>
    /// Methods for generating access tokens and refresh tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrimaryUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IRandomGenerator _randomGenerator;
        private readonly UserManager<User> _userManager;

        public TokenService(
            IHttpContextAccessor httpContextAccessor,
            IPrimaryUnitOfWork primaryUnitOfWork,
            IConfiguration configuration,
            IRandomGenerator randomGenerator,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = primaryUnitOfWork;
            _configuration = configuration;
            _randomGenerator = randomGenerator;
            _userManager = userManager;
        }


        public async Task<string> GetAccessTokenFromUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<string> GetAccessTokenFromUsernameAsync(string username)
        {
            var userId = await _unitOfWork.UserRepository.GetIdByUsernameAsync(username, _httpContextAccessor.HttpContext.RequestAborted);
            if (userId == Guid.Empty)
                return null;

            return await GetAccessTokenFromUserIdAsync(userId);
        }
    }
}