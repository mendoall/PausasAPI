using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Application.Usuarios;
public record CurrentUser(string Id, string Email, string? Nombre, string? EmpresaId, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
