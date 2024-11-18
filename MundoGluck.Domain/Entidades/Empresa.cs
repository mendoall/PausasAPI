using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Direccion { get; set; } = default!;

        }
}
