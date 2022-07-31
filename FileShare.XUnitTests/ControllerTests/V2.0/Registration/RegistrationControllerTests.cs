using FileShare.Controllers.V2._0.Registration;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Registration.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Registration
{
    public class RegistrationControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ILogger<RegistrationController>> _mockLogger;
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRegistrationService> _mockRegistrationService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockLogger = new Mock<ILogger<RegistrationController>>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockRegistrationService = new Mock<IRegistrationService>();
            _mockTokenService = new Mock<ITokenService>();
            _controller = new RegistrationController(_mockHttpContextAccessor.Object, _mockLogger.Object, _mockUnitOfWork.Object, _mockRegistrationService.Object, _mockTokenService.Object);
        }


        [Fact]
        public async Task Register_ReturnsCreatedResult()
        {
            // Arrange
            _mockRegistrationService.Setup(service => service.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(value: new(Guid.NewGuid(), Guid.NewGuid()));

            _mockTokenService.Setup(service => service.GetAccessToken(It.IsAny<Guid>()))
                .Returns(string.Empty);

            _mockTokenService.Setup(service => service.GetRefreshTokenAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(string.Empty);

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            // Act
            var result = await _controller.Register(new(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.NotNull(createdResult.Value);
        }
    }
}