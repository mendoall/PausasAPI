using MundoGluck.Domain.Entidades;

namespace MundoGluck.Domain.Repositorios
{
    public interface IEmpresaRepositorio
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa?> GetItemByIdAsync(int id);
        Task<int> AddItemAsync(Empresa item);
        Task ModifyItemAsync(Empresa item);
        Task DeleteItemAsync(int id);
    }
}
