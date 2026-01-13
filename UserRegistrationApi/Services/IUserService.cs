namespace UserRegistrationApi.Services;

using UserRegistrationApi.Models;

public interface IUserService
{
    Task<bool> RegisterUserAsync(UserDto user);
}