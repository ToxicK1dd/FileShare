using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.RefreshToken;
using FileShare.Utilities.Generators.Random.Interface;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.V2._0.RefreshToken
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
        public async Task ValidateRefreshToken_ShouldReturnString_WhenOldTokenIsValid()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken() { Expires = DateTime.Now.AddDays(1) });

            _mockRandomGenerator.Setup(generator => generator.GenerateBase64String(It.IsAny<int>()))
                .Returns("12345");

            // Act
            var result = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(string.Empty, result);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRandomGenerator.Verify(generator => generator.GenerateBase64String(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnNull_WhenOldTokenIsExpired()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken() { Expires = DateTime.Now.AddDays(-1) });

            // Act
            var result = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.Null(result);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRandomGenerator.Verify(generator => generator.GenerateBase64String(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnNull_WhenOldTokenIsInvalid()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _refreshTokenService.ValidateRefreshTokenAsync(string.Empty);

            // Assert
            Assert.Null(result);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRandomGenerator.Verify(generator => generator.GenerateBase64String(It.IsAny<int>()), Times.Never);
        }
    }
}