using MundoGluck.Domain.Entidades;

namespace MundoGluck.Application.Empresas.Services
{
    public interface IEmpresaService
    {
        Task<IEnumerable<Empresa>> GetAllItems();
        Task<Empresa?> GetByID(int id);
        Task ModifyItem(Empresa item);
        Task<int> AddItem(Empresa item);
        Task DeleteItem(int id);
    }
}
