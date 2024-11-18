using Microsoft.EntityFrameworkCore;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Exceptions;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Infrastructure.Data;


namespace MundoGluck.Infrastructure.Repositorios
{
    internal class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly MundoGluck_DbContext _dbContext;
        public EmpresaRepositorio(MundoGluck_DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            var empresas = await _dbContext.Empresas.ToListAsync();
            return empresas;
        }

        public async Task<Empresa?> GetItemByIdAsync(int id)
        {
            return await _dbContext.Empresas.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> AddItemAsync(Empresa item)
        {
            _dbContext.Empresas.Add(item);
            await _dbContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task ModifyItemAsync(Empresa item)
        {
            _dbContext.Empresas.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _dbContext.Empresas.FirstOrDefaultAsync(x => x.Id == id)
                                    ?? throw new NotFoundException(nameof(Empresa), id.ToString());
            _dbContext.Empresas.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
