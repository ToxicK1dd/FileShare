using FileShare.DataAccess.Models.Primary.User;
using FileShare.Service.Dtos.V2._0.Registration;
using FileShare.Service.Services.V2._0.Registration;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace FileShare.XUnitTests.ServiceTests.V2._0.Registration
{
    public class RegistrationServiceTests
    {
        private readonly Mock<IUserStore<User>> _mockUserStore;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IRoleStore<IdentityRole<Guid>>> _mockRoleStore;
        private readonly Mock<RoleManager<IdentityRole<Guid>>> _mockRoleManager;
        private readonly RegistrationService _registrationService;

        public RegistrationServiceTests()
        {
            _mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(_mockUserStore.Object, null, null, null, null, null, null, null, null);
            _mockRoleStore = new Mock<IRoleStore<IdentityRole<Guid>>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>(_mockRoleStore.Object, null, null, null, null);

            _registrationService = new RegistrationService(_mockUserManager.Object, _mockRoleManager.Object);
        }


        [Fact]
        public async Task Register_Should_Return_Dto_When_User_Is_Registered()
        {
            // Arrange
            User user = null;
            var username = "Test";
            var password = "!Test1234";
            var email = "test@test.test";

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _mockUserManager.Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _mockRoleManager.Setup(manager => manager.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            _mockRoleManager.Setup(manager => manager.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
               .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Callback<User, string>((a, s) => user = a)
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _registrationService.RegisterAsync(username, email, password, CancellationToken.None);

            // Assert
            var dto = Assert.IsType<RegistrationResultDto>(result);
            Assert.True(dto.Successful);

            Assert.Equal(username, user.UserName);
            Assert.Equal(email, user.Email);
            Assert.True(user.Enabled);
            Assert.False(user.Verified);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mockRoleManager.Verify(manager => manager.RoleExistsAsync(It.IsAny<string>()), Times.Exactly(2));
            _mockRoleManager.Verify(manager => manager.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Once);
            _mockUserManager.Verify(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Email_Is_Invalid()
        {
            // Act
            var result = await _registrationService.RegisterAsync("test", "test", "!Test1234", CancellationToken.None);

            // Assert
            Assert.False(result.Successful);
            Assert.Equal("Email is not valid format.", result.ErrorMessage);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mockRoleManager.Verify(manager => manager.RoleExistsAsync(It.IsAny<string>()), Times.Never);
            _mockRoleManager.Verify(manager => manager.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Username_Is_Taken()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            // Act
            var result = await _registrationService.RegisterAsync("test", "test@test.test", "!Test1234", CancellationToken.None);

            // Assert
            Assert.False(result.Successful);
            Assert.Equal("Username is already taken.", result.ErrorMessage);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Never);
            _mockRoleManager.Verify(manager => manager.RoleExistsAsync(It.IsAny<string>()), Times.Never);
            _mockRoleManager.Verify(manager => manager.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Register_Should_Throw_When_Email_Is_Taken()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            _mockUserManager.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(value: null);

            _mockUserManager.Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(value: new());

            // Act
            var result = await _registrationService.RegisterAsync("test", "test@test.test", "!Test1234", CancellationToken.None);

            // Assert
            Assert.False(result.Successful);
            Assert.Equal("Email is already taken.", result.ErrorMessage);

            _mockUserManager.Verify(manager => manager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            _mockUserManager.Verify(manager => manager.FindByEmailAsync(It.IsAny<string>()), Times.Once);
            _mockRoleManager.Verify(manager => manager.RoleExistsAsync(It.IsAny<string>()), Times.Never);
            _mockRoleManager.Verify(manager => manager.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Never);
            _mockUserManager.Verify(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
            _mockUserManager.Verify(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }
    }
}