using FileShare.DataAccess.Models.Primary.Login;
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
        private readonly Mock<IPasswordHasher<object>> _mockPasswordHasher;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockIdentityClaimsHelper = new Mock<IIdentityClaimsHelper>();
            _mockRandomGenerator = new Mock<IRandomGenerator>();
            _mockPasswordHasher = new Mock<IPasswordHasher<object>>();

            _loginService = new LoginService(
                _mockHttpContextAccessor.Object,
                _mockUnitOfWork.Object,
                _mockIdentityClaimsHelper.Object,
                _mockRandomGenerator.Object,
                _mockPasswordHasher.Object);
        }


        [Fact]
        public async Task ValidateCredentials_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            string username = "Test";
            string password = "!Test1234";
            string hashedPassword = new PasswordHasher<object>().HashPassword(null, password);
            CancellationToken cancellationToken = CancellationToken.None;

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(x => x.LoginRepository.GetFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.Login.Login() { Password = hashedPassword });

            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            // Act
            var result = await _loginService.ValidateCredentialsAsync(username, password, cancellationToken);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateCredentials_ShouldReturnFalse_WhenCredentialsAreInvalid()
        {
            // Arrange
            string username = "Test";
            string password = "!Test1234";
            string hashedPassword = new PasswordHasher<object>().HashPassword(null, password);
            CancellationToken cancellationToken = CancellationToken.None;

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(x => x.LoginRepository.GetFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.Login.Login() { Password = hashedPassword });

            // Act
            var result = await _loginService.ValidateCredentialsAsync(username, "InvalidPassword", cancellationToken);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateCredentials_ShouldReturnFalse_WhenLoginDoesNotExist()
        {
            // Arrange
            string username = "Test";
            string password = "!Test1234";
            string hashedPassword = new PasswordHasher<object>().HashPassword(null, password);
            CancellationToken cancellationToken = CancellationToken.None;

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockUnitOfWork.Setup(x => x.LoginRepository.GetFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((DataAccess.Models.Primary.Login.Login)null);

            // Act
            var result = await _loginService.ValidateCredentialsAsync(username, password, cancellationToken);

            // Assert
            Assert.False(result);
            _mockUnitOfWork.Verify(repo => repo.LoginRepository.GetFromUsernameAsync(It.IsAny<string>(), cancellationToken), Times.Once);
        }


        [Fact]
        public async Task ChangeCredentials_ShouldReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            string password = "!Test1234";
            CancellationToken cancellationToken = CancellationToken.None;

            _mockHttpContextAccessor.Setup(accessor => accessor.HttpContext)
                .Returns(new DefaultHttpContext());

            _mockIdentityClaimsHelper.Setup(helper => helper.GetAccountIdFromHttpContext(It.IsAny<HttpContext>()))
                .Returns(Guid.NewGuid());

            _mockUnitOfWork.Setup(x => x.LoginRepository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DataAccess.Models.Primary.Login.Login());

            _mockPasswordHasher.Setup(hasher => hasher.VerifyHashedPassword(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(It.IsAny<object>(), It.IsAny<string>()))
                .Returns(password);

            // Act
            var result = await _loginService.ChangeCredentialsAsync(password, password, cancellationToken);

            // Assert
            Assert.True(result);
        }
    }
}