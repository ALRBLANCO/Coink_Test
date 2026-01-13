namespace UserRegistrationApi.Models;

// UserDto sin StateId, ni CountryId para evitar inconsistecias
public record UserDto(
    string FullName,
    string Phone,
    string Address,
    int CityId
);