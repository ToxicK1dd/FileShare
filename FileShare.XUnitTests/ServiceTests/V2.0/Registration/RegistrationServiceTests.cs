using FileShare.DataAccess.UnitOfWork.Primary.Interface;
using FileShare.Service.Services.V2._0.Registration;
using MapsterMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.V2._0.Registration
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IPrimaryUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockUnitOfWork = new Mock<IPrimaryUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _registrationService = new RegistrationService(_mockUnitOfWork.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task Register_Should_Return_Guids_When_User_Is_Registered()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUnitOfWork.Setup(repo => repo.LoginRepository.ExistsFromUsernameAsync(It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(false);

            _mockUnitOfWork.Setup(repo => repo.EmailRepository.ExistsFromAddressAsync(It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(false);

            _mockUnitOfWork.Setup(repo => repo.AccountRepository.AddAsync(It.IsAny<DataAccess.Models.Primary.Account.Account>(), cancellationToken))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var (loginId, accountId) = await _registrationService.RegisterAsync("Test", "test@test.test", "!Test1234", cancellationToken);

            // Assert
            Assert.IsType<Guid>(accountId);
            Assert.IsType<Guid>(loginId);
            Assert.NotEqual(Guid.Empty, accountId);
            Assert.NotEqual(Guid.Empty, loginId);

            _mockUnitOfWork.Verify();
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