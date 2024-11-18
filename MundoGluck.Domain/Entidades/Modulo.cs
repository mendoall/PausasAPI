using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public DateTime FechaInico { get; set; }
        public DateTime FechaFin { get; set; }

        public ICollection<Actividad> actividades { get; set; } = new List<Actividad>();

    }

}
