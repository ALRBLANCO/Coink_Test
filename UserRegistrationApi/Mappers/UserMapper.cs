using UserRegistrationApi.Domain.Entities;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Mappers;

public static class UserMapper
{
    public static User ToEntity(UserDto dto)
    {
        return new User(
            dto.FullName,
            dto.Phone,
            dto.Address,
            dto.CityId
        );
    }

    public static UserDto ToDto(User entity)
    {
        return new UserDto(
            entity.FullName,
            entity.Phone,
            entity.Address,
            entity.CityId
        );
    }
}