using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MundoGluck.Domain.Entidades;
using MundoGluck.Infrastructure.Data;
using MundoGluck.Infrastructure.Extensions;
using MundoGluck.Infrastructure.Seeders;
using MundoGluck.Application.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using MundoGluck.Infrastructure.Autorizacion;

var builder = WebApplication.CreateBuilder(args);


// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});


// Add services to the container.
builder.Services.AddAuthentication();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Id = "bearerAuth", Type = ReferenceType.SecurityScheme }
            },new string [] {}
        }
    }
    );
});
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("MundoGluck_Db");

builder.Services.AddDbContext<MundoGluck_DbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentityApiEndpoints<Usuario>()
    .AddRoles<IdentityRole>()
    .AddClaimsPrincipalFactory<MundoGluckUserClaimsPrincipalFactory>()
    .AddEntityFrameworkStores<MundoGluck_DbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAplication();
builder.Services.AddInfrastructure();


var app = builder.Build();

// Crear un ámbito y ejecutar el seeder de Empresa
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IRolSeeder>();
    await seeder.Seed();
}
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IEmpresaSeeder>();
    await seeder.Seed();
}

using (var scopemodulo = app.Services.CreateScope())
{
    var seedermodulo = scopemodulo.ServiceProvider.GetRequiredService<IModuloSeeder>();
    await seedermodulo.Seed();
}
using (var scopeactividad = app.Services.CreateScope())
{
    var seederactividad = scopeactividad.ServiceProvider.GetRequiredService<IActividadSeeder>();
    await seederactividad.Seed();
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseSwagger();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("v1/api/identity").MapIdentityApi<Usuario>();
app.MapControllers();

app.Run();
