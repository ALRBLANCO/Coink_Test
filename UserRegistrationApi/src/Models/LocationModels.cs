namespace UserRegistrationApi.src.Models;

// Agregamos un constructor vacío o usamos propiedades con { get; init; }
public record CountryDto(int Id, string Name);

public record StateDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int CountryId { get; init; }
}

public record CityDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int StateId { get; init; }
}