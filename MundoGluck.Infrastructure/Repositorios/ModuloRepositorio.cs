using Microsoft.EntityFrameworkCore;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Exceptions;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Infrastructure.Data;


namespace MundoGluck.Infrastructure.Repositorios
{
    internal class ModuloRepositorio : IModuloRepositorio
    {
        private readonly MundoGluck_DbContext _dbContext;
        public ModuloRepositorio(MundoGluck_DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Modulo>> GetAllAsync()
        {
            var Modulos = await _dbContext.Modulos.ToListAsync();
            return Modulos;
        }

        public async Task<Modulo?> GetItemByIdAsync(int id)
        {
            return await _dbContext.Modulos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> AddItemAsync(Modulo item)
        {
            _dbContext.Modulos.Add(item);
            await _dbContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task ModifyItemAsync(Modulo item)
        {
            _dbContext.Modulos.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _dbContext.Modulos.FirstOrDefaultAsync(x => x.Id == id)
                                    ?? throw new NotFoundException(nameof(Modulo), id.ToString());
            _dbContext.Modulos.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
