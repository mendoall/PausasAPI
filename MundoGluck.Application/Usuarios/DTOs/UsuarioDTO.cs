using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Application.Usuarios.DTOs
{
    public class UsuarioDTO
    {
        public string Email { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public int EmpresaId { get; set; }
        public string TipoUsuario { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
