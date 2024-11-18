using MundoGluck.Application.Usuarios.DTOs;
using MundoGluck.Domain.Entidades;


namespace MundoGluck.Application.Usuarios.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllItems();
        Task<Usuario?> GetByID(string id);
        Task ModifyItem(UsuarioDTO item);
        Task<int> AddItem(Usuario item);
        Task DeleteItem(string id);
        Task<System.Net.HttpStatusCode> ForgotPassword(ForgotPasswordDTO emailDto);
        Task assingUserRole(AssignUserDTO assignUserDto);

    }
}
