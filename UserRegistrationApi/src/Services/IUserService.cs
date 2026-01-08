namespace UserRegistrationApi.src.Services;

using UserRegistrationApi.src.Models;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserDto user);
}