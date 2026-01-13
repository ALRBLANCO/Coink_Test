namespace UserRegistrationApi.Services;

using UserRegistrationApi.Data;
using UserRegistrationApi.Models;
using UserRegistrationApi.Mappers;

public class UserService(IUserRepository userRepository, IMasterRepository masterRepository) : IUserService
{
    public async Task<bool> RegisterUserAsync(UserDto userDto)
    {
        var user = UserMapper.ToEntity(userDto);

        // Validación de existencia física mediante el SP de validación (o consulta directa)
        if (!await masterRepository.CityExistsAsync(user.CityId))
        {
            throw new KeyNotFoundException($"The city with ID {user.CityId} don't exist into the system.");
        }

        // Si existe, procedemos al registro
        await userRepository.RegisterUserAsync(user);
        return true;
    }
}