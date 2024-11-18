using Microsoft.Extensions.DependencyInjection;
using MundoGluck.Application.Actividades.Services;
using MundoGluck.Application.Empresas.Services;
using MundoGluck.Application.Modulos.Services;
using MundoGluck.Application.Usuarios.Services;


namespace MundoGluck.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IActividadesService, ActividadesService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IModuloService, ModuloService>();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();
    }
}
