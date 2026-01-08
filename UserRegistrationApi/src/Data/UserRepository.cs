using Dapper;
using Npgsql;
using System.Data;
using UserRegistrationApi.src.Models;

namespace UserRegistrationApi.src.Data;

public class UserRepository(IConfiguration configuration) : IUserRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") 
        ?? throw new ArgumentNullException("Connection string is missing");

    public async Task RegisterUserAsync(UserDto user)
    {
        try
        {
            using IDbConnection db = new NpgsqlConnection(_connectionString);
        
            var parameters = new {
                p_full_name = user.FullName,
                p_phone = user.Phone,
                p_address = user.Address,
                p_city_id = user.CityId
            };

            // Call PostgreSQL Stored Procedure
            await db.ExecuteAsync("CALL sp_register_user(@p_full_name, @p_phone, @p_address, @p_city_id)", parameters);
        }
        catch (PostgresException ex) when (ex.SqlState == "28P01")
        {
            throw new Exception("Error de autenticación: La contraseña en appsettings.json no coincide con la de Docker.");
        }
        catch (PostgresException ex) when (ex.SqlState == "23503")
        {
            throw new Exception("Error de integridad: El CityId proporcionado no existe en la base de datos.");
        }
        
    }
}