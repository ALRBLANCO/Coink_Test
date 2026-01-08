namespace UserRegistrationApi.src.Services;

using UserRegistrationApi.src.Data;
using UserRegistrationApi.src.Models;
using UserRegistrationApi.src.Mappers;

public class UserService(IUserRepository userRepository, IMasterRepository masterRepository) : IUserService
{
    public async Task<bool> RegisterUserAsync(UserDto user)
    {
        // Validación de existencia física mediante el SP de validación (o consulta directa)
        if (!await masterRepository.CityExistsAsync(user.CityId))
        {
            throw new KeyNotFoundException($"La ciudad con ID {user.CityId} no existe en el sistema.");
        }

        // Si existe, procedemos al registro
        await userRepository.RegisterUserAsync(user);
        return true;
    }
}