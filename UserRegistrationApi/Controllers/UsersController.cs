using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using UserRegistrationApi.Models;
using UserRegistrationApi.Services;

namespace UserRegistrationApi.Controllers;

//versionado estatico por simplicidad
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUserService userService, IValidator<UserDto> validator, ILogger<UsersController> logger) : ControllerBase
{

    // El dto no trae idState, ni idCountry ya que he pensado en la integridad referencial
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
        // 1. Log de inicio de petición
        logger.LogInformation("Iniciando registro para el usuario: {FullName}", request.FullName);

        // 2. Validación
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Validación fallida para {FullName}: {Errors}", 
                request.FullName, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors);
        }

        // 3. Ejecución de lógica de negocio
        // Si falla, el Middleware captura la excepción y la loguea automáticamente
        await userService.RegisterUserAsync(request);
        
        // 4. Log de éxito
        logger.LogInformation("Usuario {FullName} registrado exitosamente.", request.FullName);
        
        return Ok(new { Message = "User registered successfully" });
    }
}