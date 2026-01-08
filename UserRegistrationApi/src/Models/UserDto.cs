namespace UserRegistrationApi.src.Models;

public record UserDto(
    string FullName,
    string Phone,
    string Address,
    int CityId
);