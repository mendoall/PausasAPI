using MundoGluck.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetItemByIdAsync(string id);
        Task<int> AddItemAsync(Usuario item);
        Task DeleteItemAsync(string id);
        Task<Usuario?> FindByEmailAsync(string email);
        Task ModifyItemAsync(Usuario item);
    }
}
