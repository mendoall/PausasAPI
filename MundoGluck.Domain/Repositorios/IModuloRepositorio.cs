using MundoGluck.Domain.Entidades;

namespace MundoGluck.Domain.Repositorios
{
    public interface IModuloRepositorio
    {
        Task<IEnumerable<Modulo>> GetAllAsync();
        Task<Modulo?> GetItemByIdAsync(int id);
        Task<int> AddItemAsync(Modulo item);
        Task ModifyItemAsync(Modulo item);
        Task DeleteItemAsync(int id);
    }
}
