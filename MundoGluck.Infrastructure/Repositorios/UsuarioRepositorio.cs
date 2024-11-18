using MundoGluck.Infrastructure.Data;
using MundoGluck.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Domain.Exceptions;
using System.Threading.Tasks;

namespace MundoGluck.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly MundoGluck_DbContext _dbContext;

        public UsuarioRepositorio(MundoGluck_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Usuario?> GetItemByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> AddItemAsync(Usuario item)
        {
            item.Empresa = await _dbContext.Empresas.FirstOrDefaultAsync(x => Convert.ToInt32(x.Id) == item.EmpresaId);
            _dbContext.Users.Add(item);
            await _dbContext.SaveChangesAsync();

            return Convert.ToInt32(item.Id);
        }

        public async Task DeleteItemAsync(string id)
        {
            var item = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id)
                            ?? throw new NotFoundException(nameof(Usuario), id.ToString());

            _dbContext.Users.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Usuario?> FindByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task ModifyItemAsync(Usuario item)
        {
            _dbContext.Users.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
