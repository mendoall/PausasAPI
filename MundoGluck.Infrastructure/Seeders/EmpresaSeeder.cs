using MundoGluck.Domain.Entidades;
using MundoGluck.Infrastructure.Data;

namespace MundoGluck.Infrastructure.Seeders

{
    internal class EmpresaSeeder(MundoGluck_DbContext dbContext) : IEmpresaSeeder
    {


        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Empresas.Any())
                {
                    var empresas = GetEmpresa();
                    dbContext.Empresas.AddRange(empresas);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Empresa> GetEmpresa()
        {
            List<Empresa> empresas = [
                new ()
                {
            
                    Nombre = "Empresa 1",
                    Direccion = "Dirección 1",
                    
                },
                new ()
                {
                
                    Nombre = "Empresa 2",
                    Direccion = "Dirección 2",
                    
                }
                ];
            return empresas;
        }
    }

}
