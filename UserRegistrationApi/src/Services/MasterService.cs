namespace UserRegistrationApi.src.Services;

using UserRegistrationApi.src.Data;
using UserRegistrationApi.src.Models;

public class MasterService(IMasterRepository repository) : IMasterService
{
    public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync() => await repository.GetCountriesAsync();

    public async Task<IEnumerable<StateDto>> GetStatesByCountryAsync(int countryId)
    {
        if (!await repository.CountryExistsAsync(countryId))
            throw new KeyNotFoundException($"The country with ID {countryId} don't exist.");
            
        return await repository.GetStatesByCountryAsync(countryId);
    }

    public async Task<IEnumerable<CityDto>> GetCitiesByStateAsync(int stateId)
    {
        if (!await repository.StateExistsAsync(stateId))
            throw new KeyNotFoundException($"The state with ID {stateId} don't existe.");

        return await repository.GetCitiesByStateAsync(stateId);
    }
}