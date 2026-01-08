using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Data;

public interface IUserRepository
{
    Task RegisterUserAsync(UserDto user);
}