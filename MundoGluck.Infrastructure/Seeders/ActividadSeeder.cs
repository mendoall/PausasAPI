using MundoGluck.Domain.Entidades;
using MundoGluck.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Infrastructure.Seeders
{
    internal class ActividadSeeder(MundoGluck_DbContext dbContext): IActividadSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Actividades.Any())
                {
                    var actividades = GetActividad();
                    dbContext.Actividades.AddRange(actividades);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Actividad> GetActividad()
        {
            Usuario usuario = new Usuario()
            {
                Email = "ejemplo@ejemplo.com"
            };

            List<Actividad> actividades = [
                new (){ Contenido = "Elevar la mano derecha",FechaCreacion = DateTime.Now,ModuloId=1, Usuario = usuario,ArchivoActividadUrl="", LinkActividad=""},

                new (){Contenido = "Elevar el pie derecho",FechaCreacion = DateTime.Now,ModuloId=2, Usuario = usuario,ArchivoActividadUrl="", LinkActividad=""}
                ];
            return actividades;



        }
    }
}
