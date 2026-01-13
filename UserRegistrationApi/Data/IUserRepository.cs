using UserRegistrationApi.Domain.Entities;

namespace UserRegistrationApi.Data;

public interface IUserRepository
{
    Task RegisterUserAsync(User user);
}