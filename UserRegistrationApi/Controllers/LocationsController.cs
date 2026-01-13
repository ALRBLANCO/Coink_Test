using Microsoft.AspNetCore.Mvc;
using UserRegistrationApi.Services;

namespace UserRegistrationApi.Controllers;

//versionado estatico por simplicidad
[ApiController]
[Route("api/v1/[controller]")]
public class LocationsController(IMasterService masterService) : ControllerBase
{
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries() => Ok(await masterService.GetAllCountriesAsync());

    [HttpGet("countries/{countryId}/states")]
    public async Task<IActionResult> GetStates(int countryId) => Ok(await masterService.GetStatesByCountryAsync(countryId));

    [HttpGet("states/{stateId}/cities")]
    public async Task<IActionResult> GetCities(int stateId) => Ok(await masterService.GetCitiesByStateAsync(stateId));
}