using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MundoGluck.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MundoGluck.Infrastructure.Seeders;
using MundoGluck.Infrastructure.Repositorios;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Domain.EmailSender;
using MundoGluck.Infrastructure.SendGrid;
using Microsoft.AspNetCore.Identity;

namespace MundoGluck.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {

            services.AddScoped<IRolSeeder, RolSeeder>();
            services.AddScoped<IEmpresaSeeder,EmpresaSeeder>();
            services.AddScoped<IActividadSeeder, ActividadSeeder>();
            services.AddScoped<IModuloSeeder, ModuloSeeder>();
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IActividadRepositorio, ActividadesRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IModuloRepositorio, ModuloRepositorio>();
            services.AddSingleton<IEmailSender, SendGridEmailSender>();
        }
    }
}
