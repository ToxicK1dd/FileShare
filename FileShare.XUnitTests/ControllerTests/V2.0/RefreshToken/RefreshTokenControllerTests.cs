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
        public async Task Revoke_ShouldReturnNoContent_WhenTokenIsValid()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken());

            _mockRefreshTokenService.Setup(service => service.RevokeRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Revoke(It.IsAny<string>());

            // Assert
            Assert.IsType<NoContentResult>(result);

            _mockRefreshTokenService.Verify(service => service.RevokeRefreshTokenAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Revoke_ShouldReturnNotFound_WhenTokenIsInvalid()
        {
            // Arrange
            _mockRefreshTokenService.Setup(service => service.RevokeRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Revoke(It.IsAny<string>());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);

            _mockRefreshTokenService.Verify(service => service.RevokeRefreshTokenAsync(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task Refresh_ShouldReturnCreated_WhenTokenIsValid()
        {
            // Arrange
            _mockRefreshTokenService.Setup(service => service.ValidateRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

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
        public async Task Refresh_ShouldReturnNotFound_WhenTokenIsInvalid()
        {
            // Arrange
            _mockRefreshTokenService.Setup(service => service.ValidateRefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Refresh(It.IsAny<string>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}