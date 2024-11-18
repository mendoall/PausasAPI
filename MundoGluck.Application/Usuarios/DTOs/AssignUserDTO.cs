using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Application.Usuarios.DTOs;
public class AssignUserDTO
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
