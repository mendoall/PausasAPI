using MundoGluck.Domain.Entidades;
using MundoGluck.Infrastructure.Data;


namespace MundoGluck.Infrastructure.Seeders
{
    internal class ModuloSeeder(MundoGluck_DbContext dbContext) : IModuloSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Modulos.Any())
                {
                    var modulos = GetModulo();
                    dbContext.Modulos.AddRange(modulos);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Modulo> GetModulo()
        {
            List<Modulo> modulos = [
                new () {Nombre="Movimiento superior",Descripcion="Se realizan movimientos de la parte superior del cuerpo",FechaInico=DateTime.Now,FechaFin=DateTime.Now.AddDays(4)},
                 new () {Nombre="Movimiento inferior",Descripcion="Se realizan movimientos de la parte inferior del cuerpo",FechaInico=DateTime.Now,FechaFin=DateTime.Now.AddDays(4)}
                ];
            return modulos;
        }
    }
}
