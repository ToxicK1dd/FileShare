using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Dtos.V2._0.Registration;
using FileShare.Service.Services.V2._0.Registration;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.V2._0.Registration
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher<object>> _mockPasswordHasher;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher<object>>();
            _mockMapper = new Mock<IMapper>();
            _registrationService = new RegistrationService(_mockUnitOfWork.Object, _mockPasswordHasher.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task Register_Should_Return_Guids_When_User_Is_Registered()
        {
            // Arrange
            DataAccess.Models.Primary.Account.Account account = null;
            var username = "Test";
            var password = "!Test1234";
            var email = "test@test.test";

            _mockUnitOfWork.Setup(repo => repo.LoginRepository.ExistsFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockUnitOfWork.Setup(repo => repo.EmailRepository.ExistsFromAddressAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _mockPasswordHasher.Setup(hasher => hasher.HashPassword(It.IsAny<object>(), It.IsAny<string>()))
                .Returns(password);

            _mockUnitOfWork.Setup(repo => repo.AccountRepository.AddAsync(It.IsAny<DataAccess.Models.Primary.Account.Account>(), It.IsAny<CancellationToken>()))
                .Callback<DataAccess.Models.Primary.Account.Account, CancellationToken>((a, c) => account = a)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _registrationService.RegisterAsync(username, email, password, CancellationToken.None);

            // Assert
            var dto = Assert.IsType<RegistrationResultDto>(result);
            Assert.NotEqual(Guid.Empty, dto.AccountId);
            Assert.NotEqual(Guid.Empty, dto.LoginId);

            Assert.Equal(dto.LoginId, account.Login.Id);
            Assert.Equal(dto.AccountId, account.Login.AccountId);
            Assert.Equal(username, account.Login.Username);
            Assert.Equal(password, account.Login.Password);
            Assert.Equal(email, account.Email.Address);
            Assert.Equal(dto.AccountId, account.Id);
            Assert.True(account.Enabled);
            Assert.False(account.Verified);

            _mockUnitOfWork.Verify(repo => repo.LoginRepository.ExistsFromUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(repo => repo.EmailRepository.ExistsFromAddressAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockPasswordHasher.Verify(hasher => hasher.HashPassword(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
            _mockUnitOfWork.Verify(repo => repo.AccountRepository.AddAsync(It.IsAny<DataAccess.Models.Primary.Account.Account>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Email_Is_Invalid()
        {
            // Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(
                () => _registrationService.RegisterAsync(It.IsAny<string>(), "test", It.IsAny<string>(), CancellationToken.None));

            // Assert
            Assert.Equal("Email is not in a valid format.", ex.Message);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Username_Is_Taken()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUnitOfWork.Setup(repo => repo.LoginRepository.ExistsFromUsernameAsync(It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(true);

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(
                () => _registrationService.RegisterAsync(It.IsAny<string>(), "test@test.test", It.IsAny<string>(), cancellationToken));

            // Assert
            Assert.Equal("Username is already being used.", ex.Message);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Email_Is_Taken()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUnitOfWork.Setup(repo => repo.EmailRepository.ExistsFromAddressAsync(It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(true);

            _mockUnitOfWork.Setup(repo => repo.LoginRepository.ExistsFromUsernameAsync(It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(false);

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentException>(
                () => _registrationService.RegisterAsync(It.IsAny<string>(), "test@test.test", It.IsAny<string>(), cancellationToken));

            // Assert
            Assert.Equal("Email is already being used.", ex.Message);
        }
    }
}