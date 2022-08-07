using FileShare.DataAccess.Models.Primary.User;
using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Login;
using FileShare.Utilities.Generators.Random.Interface;
using FileShare.Utilities.Helpers.IdentityClaims.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.V2._0.Login
{
    public class LoginServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IIdentityClaimsHelper> _mockIdentityClaimsHelper;
        private readonly Mock<IRandomGenerator> _mockRandomGenerator;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly Mock<IUserStore<User>> _mockUserStore;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockIdentityClaimsHelper = new Mock<IIdentityClaimsHelper>();
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, _mockPasswordHasher.Object, null, null, null, null, null, null);

            _loginService = new LoginService(
                _mockHttpContextAccessor.Object,
                _mockUnitOfWork.Object,
                _mockIdentityClaimsHelper.Object,
                _mockRandomGenerator.Object,
                _mockUserManager.Object);
        }


        [Fact]
        public async Task ValidateCredentials_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            _mockUserManager.Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _loginService.ValidateCredentialsByUsernameAsync("Test", "!Test1234");

            // Assert
            Assert.True(result);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ValidateCredentials_ShouldReturnFalse_WhenCredentialsAreInvalid()
        {
            // Arrange
            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            _mockUserManager.Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _loginService.ValidateCredentialsByUsernameAsync("Test", "InvalidPassword");

            // Assert
            Assert.False(result);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ValidateCredentials_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            // Act
            var result = await _loginService.ValidateCredentialsByUsernameAsync("Test", "!Test1234");

            // Assert
            Assert.False(result);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task ChangeCredentials_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockIdentityClaimsHelper.Setup(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()))
                .Returns("Test");

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            _mockUserManager.Setup(manager => manager.RemovePasswordAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(manager => manager.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _loginService.ChangeCredentialsAsync("!Test1234", "!Test1234");

            // Assert
            Assert.True(result);

            _mockHttpContextAccessor.Verify(accessor => accessor.HttpContext, Times.Once);
            _mockIdentityClaimsHelper.Verify(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.RemovePasswordAsync(It.IsAny<User>()), Times.Once);
            _mockUserManager.Verify(manager => manager.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ChangeCredentials_ShouldReturnFalse_WhenHttpContextIsInvalid()
        {
            // Arrange
            _mockIdentityClaimsHelper.Setup(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()))
                .Returns(value: null);

            // Act
            var result = await _loginService.ChangeCredentialsAsync("!Test1234", "!Test1234");

            // Assert
            Assert.False(result);

            _mockHttpContextAccessor.Verify(accessor => accessor.HttpContext, Times.Once);
            _mockIdentityClaimsHelper.Verify(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Never);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.RemovePasswordAsync(It.IsAny<User>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ChangeCredentials_ShouldReturnFalse_WhenUserIsNotFound()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockIdentityClaimsHelper.Setup(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()))
                .Returns("Test");

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);


            // Act
            var result = await _loginService.ChangeCredentialsAsync("!Test1234", "!Test1234");

            // Assert
            Assert.False(result);

            _mockHttpContextAccessor.Verify(accessor => accessor.HttpContext, Times.Once);
            _mockIdentityClaimsHelper.Verify(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.RemovePasswordAsync(It.IsAny<User>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ChangeCredentials_ShouldReturnFalse_WhenPasswordIsInvalid()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockIdentityClaimsHelper.Setup(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()))
                .Returns("Test");

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            var result = await _loginService.ChangeCredentialsAsync("!Test1234", "!Test1234");

            // Assert
            Assert.False(result);

            _mockHttpContextAccessor.Verify(accessor => accessor.HttpContext, Times.Once);
            _mockIdentityClaimsHelper.Verify(helper => helper.GetUsernameFromHttpContext(It.IsAny<HttpContext>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.VerifyHashedPassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.RemovePasswordAsync(It.IsAny<User>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnString_WhenOldTokenIsValid()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.RefreshToken.RefreshToken());

            _mockRandomGenerator.Setup(generator => generator.GenerateBase64String(It.IsAny<int>()))
                .Returns("12345");

            // Act
            var result = await _loginService.ValidateRefreshTokenAsync(string.Empty, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(string.Empty, result);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRandomGenerator.Verify(generator => generator.GenerateBase64String(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task ValidateRefreshToken_ShouldReturnNull_WhenOldTokenIsInvalid()
        {
            // Arrange
            _mockUnitOfWork.Setup(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.RefreshToken.RefreshToken)null);

            // Act
            var result = await _loginService.ValidateRefreshTokenAsync(string.Empty, CancellationToken.None);

            // Assert
            Assert.Null(result);

            _mockUnitOfWork.Verify(repo => repo.RefreshTokenRepository.GetFromTokenAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockRandomGenerator.Verify(generator => generator.GenerateBase64String(It.IsAny<int>()), Times.Never);
        }
    }
}