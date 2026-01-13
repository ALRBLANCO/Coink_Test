using Moq;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;
using UserRegistrationApi.Domain.Entities;
using UserRegistrationApi.Services;
using Xunit;

namespace UserRegistrationApi.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IMasterRepository> _masterRepoMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _masterRepoMock = new Mock<IMasterRepository>();
        _userService = new UserService(_userRepoMock.Object, _masterRepoMock.Object);
    }

    [Fact]
    public async Task RegisterUser_ShouldThrowException_WhenCityDoesNotExist()
    {
        // Arrange: Configuramos el mock para que devuelva 'false' (la ciudad no existe)
        _masterRepoMock.Setup(m => m.CityExistsAsync(It.IsAny<int>()))
                       .ReturnsAsync(false);

        var userDto = new UserDto("Test User", "3101234567", "Calle 123", 999);

        // Act & Assert: Verificamos que lance la excepción KeyNotFoundException
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _userService.RegisterUserAsync(userDto));
        
        // Verificamos que NUNCA se llamó al repositorio de usuarios
        _userRepoMock.Verify(r => r.RegisterUserAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUser_ShouldCallRepository_WhenCityExists()
    {
        // Arrange: La ciudad sí existe
        _masterRepoMock.Setup(m => m.CityExistsAsync(It.IsAny<int>()))
                       .ReturnsAsync(true);

        var userDto = new UserDto("Test User", "3101234567", "Calle 123", 1);

        // Act
        await _userService.RegisterUserAsync(userDto);

        // Assert: Verificamos que se llamó al repositorio de registro exactamente una vez
        _userRepoMock.Verify(r => r.RegisterUserAsync(It.Is<User>(u => 
                u.FullName == "Test User" && 
                u.Phone == "3101234567")
            ), Times.Once);
    }
}