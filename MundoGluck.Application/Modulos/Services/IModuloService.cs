using MundoGluck.Domain.Entidades;

namespace MundoGluck.Application.Modulos.Services
{
    public interface IModuloService
    {
        Task<IEnumerable<Modulo>> GetAllItems();
        Task<Modulo?> GetByID(int id);
        Task ModifyItem(Modulo item);
        Task<int> AddItem(Modulo item);
        Task DeleteItem(int id);
    }
}
