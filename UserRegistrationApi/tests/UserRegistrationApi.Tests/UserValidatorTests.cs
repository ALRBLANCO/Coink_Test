// using Xunit;
using UserRegistrationApi.src.Models;
using UserRegistrationApi.src.Validators;
using FluentValidation.TestHelper;

namespace UserRegistrationApi.Tests;

public class UserValidatorTests
{
    private readonly UserValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Invalid()
    {
        // Arrange: Un DTO con teléfono mal formado
        var model = new UserDto("Alfa Blanco", "abc123", "Calle 1", 1);

        // Act
        var result = _validator.TestValidate(model);

        // Assert: Debería fallar la validación del teléfono
        result.ShouldHaveValidationErrorFor(x => x.Phone);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        // Arrange: Un DTO perfecto
        var model = new UserDto("Alfa Blanco", "3001234567", "Calle 1", 1);

        // Act
        var result = _validator.TestValidate(model);

        // Assert: Todo limpio
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("")]           // Vacío
    [InlineData("Abc")]        // Muy corto
    [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")] // Muy largo (si tienes límite de 100)
    public void Validator_ShouldFail_WhenNameIsInvalid(string name)
    {
        var model = new UserDto(name, "3101234567", "Calle 123", 1);
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("letras123")]  // No numérico
    [InlineData("123")]        // Muy corto
    public void Validator_ShouldFail_WhenPhoneIsInvalid(string phone)
    {
        var model = new UserDto("Alfredo", phone, "Calle 123", 1);
        var result = _validator.Validate(model);
        Assert.False(result.IsValid);
    }
}
