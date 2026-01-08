using UserRegistrationApi.src.Domain.Entities;
using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Mappers;

public static class UserDtoMapper
{
    public static User ToDomain(this UserDto dto)
    {
        return new User(
            dto.FullName,
            dto.Phone,
            dto.Address,
            dto.CityId
        );
    }

    public static UserDto ToDto(this User entity)
    {
        return new UserDto(
            entity.FullName,
            entity.Phone,
            entity.Address,
            entity.CityId
        );
    }
}