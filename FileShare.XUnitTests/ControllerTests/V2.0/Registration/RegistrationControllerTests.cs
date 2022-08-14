using FileShare.Api.Controllers.V2._0.Registration;
using FileShare.Api.Models.V2._0.Registration;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.V2._0.Registration;
using FileShare.Service.Services.V2._0.Registration.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using FluentEmail.Core;
using Mapster;
using MapsterMapper;
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
        private readonly Mock<IMapper> _mockMapper;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockRegistrationService = new Mock<IRegistrationService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockMapper = new Mock<IMapper>();

            _controller = new RegistrationController(
                _mockUnitOfWork.Object,
                _mockRegistrationService.Object,
                _mockTokenService.Object,
                _mockMapper.Object);
        }


        [Fact]
        public async Task Register_ReturnsCreatedResult()
        {
            // Arrange
            _mockMapper.Setup(mapper => mapper.Map<RegisterDto>(It.IsAny<RegistrationModel>()))
                .Returns(value: new());

            _mockRegistrationService.Setup(service => service.RegisterAsync(It.IsAny<RegisterDto>()))
                .ReturnsAsync(value: new(true, string.Empty));

            _mockTokenService.Setup(service => service.GetAccessTokenFromUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(string.Empty);

            _mockTokenService.Setup(service => service.GetRefreshTokenFromUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.
                Register(new("Superman", "superman@kryptonmail.space", "!Krypton1t3", "!Krypton1t3"));

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.NotNull(createdResult.Value);
        }
    }
}