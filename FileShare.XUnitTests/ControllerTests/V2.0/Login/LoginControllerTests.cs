using FileShare.Controllers.V2._0.Login;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Models.V2._0.Login;
using FileShare.Service.Services.V2._0.Login.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.Login
{
    public class LoginControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ILogger<LoginController>> _mockLogger;
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ILoginService> _mockLoginService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockLogger = new Mock<ILogger<LoginController>>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockLoginService = new Mock<ILoginService>();
            _mockTokenService = new Mock<ITokenService>();
            _controller = new LoginController(_mockHttpContextAccessor.Object, _mockLogger.Object, _mockUnitOfWork.Object, _mockLoginService.Object, _mockTokenService.Object);
        }


        [Fact]
        public async Task Authenticate_Returns_Created()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockLoginService.Setup(service => service.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _mockTokenService.Setup(service => service.GetAccessTokenFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("token");
            _mockTokenService.Setup(service => service.GetRefreshTokenFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
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
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockLoginService.Setup(service => service.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Authenticate(new(It.IsAny<string>(), It.IsAny<string>()));

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }


        [Fact]
        public async Task DeleteRefreshToken_Returns_NoContent()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken());

            // Act
            var result = await _controller.DeleteRefreshToken(It.IsAny<string>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRefreshToken_Returns_NotFound()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _controller.DeleteRefreshToken(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteRefreshToken_NeverExecutes_Remove()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _controller.DeleteRefreshToken(It.IsAny<string>());

            // Assert
            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.Remove(It.IsAny<DataAccess.Models.Primary.RefreshToken.RefreshToken>()), Times.Never);
        }


        [Fact]
        public async Task UpdateRefreshToken_Returns_Created()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockLoginService.Setup(service => service.ValidateRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(string.Empty);

            _mockTokenService.Setup(service => service.GetAccessToken(It.IsAny<Guid>()))
                .Returns(string.Empty);

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetAccountIdFromToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.UpdateRefreshToken(It.IsAny<string>());

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task UpdateRefreshToken_Returns_NotFound()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockLoginService.Setup(service => service.ValidateRefreshToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string)null);

            // Act
            var result = await _controller.UpdateRefreshToken(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task ChangeCredentials_Returns_NoContent()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockLoginService.Setup(service => service.ChangeCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.ChangeCredentials(new() { NewPassword = It.IsAny<string>(), OldPassword = It.IsAny<string>() });

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}