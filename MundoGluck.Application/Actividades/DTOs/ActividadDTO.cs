using MundoGluck.Domain.Entidades;

namespace MundoGluck.Application.Actividades.DTOs
{
    public class ActividadDTO
    {
        public int Id { get; set; }
        public string Contenido { get; set; } = string.Empty;        
        public DateTime FechaCreacion { get; set; }
        public int ModuloId { get; set; }
        public string UsuarioId { get; set; } = default!;        
        public string? ModuloNombre { get; set; } = default!;
        public string? UsuarioEmail { get; set; } = default!;
        public string? ArchivoActividadUrl { get; set; } = default!;
        public string LinkActividad { get; set; } = default!;
        public string? UsuarioNombre { get; set; } = default!;


        public static Actividad? FromDTO(ActividadDTO dto)
        {
            if (dto == null) return null;

            var item = new Actividad
            {
                Contenido = dto.Contenido,
                FechaCreacion = dto.FechaCreacion,
                ModuloId = dto.ModuloId,
                UsuarioId = dto.UsuarioId,
                ArchivoActividadUrl=dto.ArchivoActividadUrl,
                LinkActividad=dto.LinkActividad,
               


            };

            return item;
        }
    }

    
}
