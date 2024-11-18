using Microsoft.AspNetCore.Identity;

namespace MundoGluck.Domain.Entidades
{
    public class Usuario : IdentityUser
    {
        public string? Nombre { get; set; }
        public int? EmpresaId { get; set; }
        public string? TipoUsuario { get; set; }
        public Empresa? Empresa { get; set; }

        public List<Actividad> Actividades { get; set; } = new List<Actividad>();


    }
}
