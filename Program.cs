using JwtBearer.Models;
using JwtBearer.Services;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();


var app = builder.Build();

app.MapGet("/", handler:(TokenService service) 
    => service.GenerateToken(
        new User(1,
            "fabriciorosanet@gmail.com",
            "fafa123",
            new []
        {
            "diretor","gerente"
        }))); 
    

app.Run();
