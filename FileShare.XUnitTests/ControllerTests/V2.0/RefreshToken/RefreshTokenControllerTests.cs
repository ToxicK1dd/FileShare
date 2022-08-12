using FileShare.Api.Controllers.V2._0.RefreshToken;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.RefreshToken.Interface;
using FileShare.Service.Services.V2._0.Token.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ControllerTests.V2._0.RefreshToken
{
    public class RefreshTokenControllerTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRefreshTokenService> _mockRefreshTokenService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly RefreshTokenController _controller;

        public RefreshTokenControllerTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockRefreshTokenService = new Mock<IRefreshTokenService>();
            _mockTokenService = new Mock<ITokenService>();
            _controller = new RefreshTokenController(_mockUnitOfWork.Object, _mockRefreshTokenService.Object, _mockTokenService.Object);
        }


        [Fact]
        public async Task DeleteRefreshToken_Returns_NoContent()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken());

            // Act
            var result = await _controller.Revoke(It.IsAny<string>());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRefreshToken_Returns_NotFound()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _controller.Revoke(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteRefreshToken_NeverExecutes_Remove()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _controller.Revoke(It.IsAny<string>());

            // Assert
            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.Remove(It.IsAny<DataAccess.Models.Primary.RefreshToken.RefreshToken>()), Times.Never);
        }


        [Fact]
        public async Task UpdateRefreshToken_Returns_Created()
        {
            // Arrange
            _mockRefreshTokenService.Setup(service => service.ValidateRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            _mockTokenService.Setup(service => service.GetAccessTokenFromUserIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(string.Empty);

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetUserIdFromToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await _controller.Refresh(It.IsAny<string>());

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task UpdateRefreshToken_Returns_NotFound()
        {
            // Arrange
            _mockRefreshTokenService.Setup(service => service.ValidateRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync((string)null);

            // Act
            var result = await _controller.Refresh(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
