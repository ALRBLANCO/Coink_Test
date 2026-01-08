using Dapper;
using Npgsql;
using System.Data;
using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Data;

public class MasterRepository(IConfiguration configuration) : IMasterRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    private IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    public async Task<IEnumerable<CountryDto>> GetCountriesAsync() 
    {
        using var db = CreateConnection();
        // Consumo de SP mediante SELECT
        return await db.QueryAsync<CountryDto>("SELECT * FROM fn_get_countries()");
    }

    public async Task<IEnumerable<StateDto>> GetStatesByCountryAsync(int countryId)
    {
        using var db = CreateConnection();
        // Importante: El alias 'AS CountryId' debe coincidir con la propiedad del Record
        return await db.QueryAsync<StateDto>(
            "SELECT id, name, country_id AS CountryId FROM fn_get_states_by_country(@countryId)", 
            new { countryId });
    }

    public async Task<IEnumerable<CityDto>> GetCitiesByStateAsync(int stateId)
    {
        using var db = CreateConnection();
        // Importante: El alias 'AS StateId' debe coincidir con la propiedad del Record
        return await db.QueryAsync<CityDto>(
            "SELECT id, name, state_id AS StateId FROM fn_get_cities_by_state(@stateId)", 
            new { stateId });
    }

    // Validaciones de existencia
    public async Task<bool> CountryExistsAsync(int id)
    {
        using var db = CreateConnection();
        return await db.ExecuteScalarAsync<bool>("SELECT fn_exists_country(@id)", new { id });
    }

    public async Task<bool> StateExistsAsync(int id)
    {
        using var db = CreateConnection();
        return await db.ExecuteScalarAsync<bool>("SELECT fn_exists_state(@id)", new { id });
    }

    public async Task<bool> CityExistsAsync(int id)
    {
        using var db = CreateConnection();
        return await db.ExecuteScalarAsync<bool>("SELECT fn_exists_city(@id)", new { id });
    }
}