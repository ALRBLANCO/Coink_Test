using FluentValidation;
using UserRegistrationApi.src.Data;
using UserRegistrationApi.src.Services;
using UserRegistrationApi.src.Validators;
using UserRegistrationApi.src.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection using the 'src' namespaces
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

var app = builder.Build();

// EL MIDDLEWARE Antes de MapControllers:
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coink API V1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}

app.MapControllers();
app.Run();
