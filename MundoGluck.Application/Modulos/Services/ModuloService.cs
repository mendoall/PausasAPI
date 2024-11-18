using Microsoft.Extensions.Logging;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Repositorios;

namespace MundoGluck.Application.Modulos.Services
{
    internal class ModuloService(IModuloRepositorio modulosRepositorio, ILogger<ModuloService> logger) : IModuloService
    {

        public async Task<IEnumerable<Modulo>> GetAllItems()
        {
            logger.LogInformation("Obteniendo todos los modulos");
            var modulos = await modulosRepositorio.GetAllAsync();
            return modulos;
        }
        public async Task<Modulo?> GetByID(int id)
        {
            return await modulosRepositorio.GetItemByIdAsync(id);
        }
        public async Task<int> AddItem(Modulo item)
        {
            var id = await modulosRepositorio.AddItemAsync(item);

            return id;
        }

        public async Task ModifyItem(Modulo item)
        {
            await modulosRepositorio.ModifyItemAsync(item);
        }

        public async Task DeleteItem(int id)
        {
            await modulosRepositorio.DeleteItemAsync(id);
        }


    }
}

