
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MundoGluck.Application.Usuarios.Services;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("Contexto de usuario no encontrado");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(u => u.Type == ClaimTypes.Role)!.Select(u => u.Value);
        var nombre = user.FindFirst(u => u.Type == "Nombre")?.Value;
        var empresaId = user.FindFirst(u => u.Type == "EmpresaId")?.Value;

        return new CurrentUser(userId, email, nombre, empresaId, roles);
    }
}
