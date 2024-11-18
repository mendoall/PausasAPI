using Microsoft.Extensions.Logging;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Repositorios;

namespace MundoGluck.Application.Empresas.Services
{
    internal class EmpresaService(IEmpresaRepositorio empresasRepositorio, ILogger<EmpresaService> logger) : IEmpresaService
    {
        
        public async Task<IEnumerable<Empresa>> GetAllItems()
        {
            logger.LogInformation("Obteniendo todas las Empresas");
            var empresas = await empresasRepositorio.GetAllAsync();
            return empresas;
        }
        public async Task<Empresa?> GetByID(int id)
        {
            return await empresasRepositorio.GetItemByIdAsync(id);
        }
        public async Task<int> AddItem(Empresa item)
        {
            var id = await empresasRepositorio.AddItemAsync(item);

            return id;
        }

        public async Task ModifyItem(Empresa item)
        {
            await empresasRepositorio.ModifyItemAsync(item);
        }

        public async Task DeleteItem(int id)
        {
            await empresasRepositorio.DeleteItemAsync(id);
        }


    }
}

