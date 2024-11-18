using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Insignia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public DateTime FechaOtorgada { get; set; }

    }
}
