using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Data;

public interface IMasterRepository
{
    Task<IEnumerable<CountryDto>> GetCountriesAsync();
    Task<IEnumerable<StateDto>> GetStatesByCountryAsync(int countryId);
    Task<IEnumerable<CityDto>> GetCitiesByStateAsync(int stateId);
    
    // Métodos de validación de existencia
    Task<bool> CountryExistsAsync(int id);
    Task<bool> StateExistsAsync(int id);
    Task<bool> CityExistsAsync(int id);
}