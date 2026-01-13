namespace UserRegistrationApi.Services;

using UserRegistrationApi.Models;

public interface IMasterService 
{
    Task<IEnumerable<CountryDto>> GetAllCountriesAsync();
    Task<IEnumerable<StateDto>> GetStatesByCountryAsync(int countryId);
    Task<IEnumerable<CityDto>> GetCitiesByStateAsync(int stateId);
}