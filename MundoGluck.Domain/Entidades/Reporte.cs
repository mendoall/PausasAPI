using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Reporte
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = default!;
        public DateTime FechaGeneracion {  get; set; }

    }
}
