using MundoGluck.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Repositorios
{
    public interface IActividadRepositorio
    {
        Task<IEnumerable<Actividad>> GetAllAsync();
        Task<Actividad?> GetItemByIdAsync(int id);
        Task<int> AddItemAsync(Actividad item);
        Task ModifyItemAsync(Actividad item);
        Task DeleteItemAsync(int id);

        Task <List<Actividad?>> GetActivididadByUsuarioIdAsync(string id);
    }
}
