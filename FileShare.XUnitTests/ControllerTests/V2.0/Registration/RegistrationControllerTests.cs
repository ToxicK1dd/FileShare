﻿using FileShare.Api.Controllers.V2._0.Registration;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Registration.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Registration
{
    public class RegistrationControllerTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRegistrationService> _mockRegistrationService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockRegistrationService = new Mock<IRegistrationService>();
            _mockTokenService = new Mock<ITokenService>();

            _controller = new RegistrationController(
                _mockUnitOfWork.Object,
                _mockRegistrationService.Object,
                _mockTokenService.Object);
        }


        [Fact]
        public async Task Register_ReturnsCreatedResult()
        {
            // Arrange
            _mockRegistrationService.Setup(service => service.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(value: new(true, string.Empty));

            _mockTokenService.Setup(service => service.GetAccessTokenFromUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(string.Empty);

            _mockTokenService.Setup(service => service.GetRefreshTokenFromUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.Register(new(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.NotNull(createdResult.Value);
        }
    }
}