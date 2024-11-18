using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MundoGluck.Domain.Entidades
{
    public class Actividad
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int ModuloId { get; set; }
        public string UsuarioId { get; set; } = string.Empty;

        public string? ArchivoActividadUrl { get; set; } = default!;
        public string LinkActividad { get; set; } = default!;

        [JsonIgnore]
        public Modulo Modulo { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!;

    }
}
