using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.RefreshToken;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.RefreshToken
{
    public class RefreshTokenServiceTest
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly RefreshTokenService _refreshTokenService;

        public RefreshTokenServiceTest()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockRandomGenerator = new Mock<IRandomGenerator>();

            _refreshTokenService = new RefreshTokenService(
                _mockHttpContextAccessor.Object,
                _mockUnitOfWork.Object,
                _mockRandomGenerator.Object);
        }


        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnTrue_WhenOldTokenIsValid()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken() { Expires = DateTime.Now.AddDays(1) });

            // Act
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.True(isValid);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnFalse_WhenOldTokenIsExpired()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken() { Expires = DateTime.Now.AddDays(-1) });

            // Act
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.False(isValid);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnFalse_WhenOldTokenIsRevoked()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken() { Expires = DateTime.Now.AddDays(1), IsRevoked = true });

            // Act
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.False(isValid);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnFalse_WhenOldTokenIsNull()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var isValid = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.False(isValid);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}