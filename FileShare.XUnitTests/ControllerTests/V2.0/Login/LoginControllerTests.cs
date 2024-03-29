﻿using FileShare.Api.Controllers.V2._0.Login;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.Login.Interface;
using FileShare.Service.Services.Token.Interface;
using FileShare.Service.Services.TotpMfa.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Login
{
    public class LoginControllerTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITotpMfaService> _mockTotpMfaService;
        private readonly Mock<ILoginService> _mockLoginService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockTotpMfaService = new Mock<ITotpMfaService>();
            _mockLoginService = new Mock<ILoginService>();
            _mockTokenService = new Mock<ITokenService>();
            _controller = new LoginController(_mockUnitOfWork.Object, _mockTotpMfaService.Object, _mockLoginService.Object, _mockTokenService.Object);
        }


        [Fact]
        public async Task Authenticate_Returns_Created()
        {
            // Arrange
            _mockLoginService.Setup(service => service.ValidateCredentialsByUsernameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _mockTokenService.Setup(service => service.GetAccessTokenFromUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync("token");
            _mockTokenService.Setup(service => service.GetRefreshTokenFromUsernameAsync(It.IsAny<string>()))
               .ReturnsAsync("refreshToken");

            // Act
            var result = await _controller.Authenticate(new(It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Authenticate_Returns_Unauthorized()
        {
            // Arrange
            _mockLoginService.Setup(service => service.ValidateCredentialsByUsernameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Authenticate(new(It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_CalledOnce_ValidateCredentials()
        {
            // Arrange
            _mockLoginService.Setup(service => service.ValidateCredentialsByUsernameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _mockTokenService.Setup(service => service.GetAccessTokenFromUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync("token");
            _mockTokenService.Setup(service => service.GetRefreshTokenFromUsernameAsync(It.IsAny<string>()))
               .ReturnsAsync("refreshToken");

            // Act
            var result = await _controller.Authenticate(new(It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            _mockLoginService.Verify(service => service.ValidateCredentialsByUsernameAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}