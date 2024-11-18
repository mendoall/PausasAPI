using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Progreso
    {
        public int Id { get; set; }
        public string ProgresoTotal { get; set; } = default!;
    } 
}
