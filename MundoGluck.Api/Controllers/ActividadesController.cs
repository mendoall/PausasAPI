using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MundoGluck.Application.Actividades.DTOs;
using MundoGluck.Application.Actividades.Services;
using MundoGluck.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO;
using System.Security.Claims;
using System.Diagnostics.CodeAnalysis;
using MundoGluck.Domain.Constants;

namespace MundoGluck.Api.Controllers
{
    [Route("v1/api/actividades")]
    [ApiController]
    [Authorize]
    public class ActividadesController(IActividadesService service) : ControllerBase
    {
        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        [HttpGet]
        [Authorize (Roles = UserRoles.Admin)]
        public async Task<ActionResult<ActividadDTO>> GetActividades()
        {
            var actividades = await service.GetAllItems();
            return Ok(actividades);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Actividad>> GetActividadById(int id)
        {
            var actividades = await service.GetByID(id);
            return Ok(actividades);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> AddActividad([FromForm] ActividadDTO itemDto, IFormFile archivoActividad)
        {

            try
            {
                if (archivoActividad != null)
                {
                    // Verifica si la carpeta "Uploads" existe; si no, créala
                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    // Define la ruta del archivo y guarda el archivo en la carpeta
                    var filePath = Path.Combine(_uploadsFolder, archivoActividad.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await archivoActividad.CopyToAsync(stream);
                    }

                    // Establece la URL del archivo cargado
                    itemDto.ArchivoActividadUrl = $"/Uploads/{archivoActividad.FileName}";
                }

                var id = await service.AddItem(itemDto);
                return CreatedAtAction(nameof(GetActividadById), new { id }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> ModifyActividad([FromForm] ActividadDTO itemDto, IFormFile? archivoActividad)
        {
            try
            {
                // Obtiene la actividad existente a partir del ID proporcionado en el DTO
                var existingItem = await service.GetByID(itemDto.Id);

                // Si no se encuentra la actividad, devuelve un código de estado 404 (No Encontrado)
                if (existingItem == null)
                {
                    return NotFound();
                }

                // Verifica si se ha proporcionado un archivo para cargar
                if (archivoActividad != null)
                {
                    // Verifica si la carpeta "Uploads" existe; si no, la crea
                    if (!Directory.Exists(_uploadsFolder))
                    {
                        Directory.CreateDirectory(_uploadsFolder);
                    }

                    // Define la ruta completa del archivo en la carpeta de uploads
                    var filePath = Path.Combine(_uploadsFolder, archivoActividad.FileName);

                    // Guarda el archivo en la carpeta de uploads
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await archivoActividad.CopyToAsync(stream);
                    }

                    // Actualiza la URL del archivo en el DTO
                    itemDto.ArchivoActividadUrl = $"/Uploads/{archivoActividad.FileName}";

                    // Si la actividad existente tiene una URL de archivo, elimina el archivo antiguo
                    if (!string.IsNullOrEmpty(existingItem.ArchivoActividadUrl) && existingItem.ArchivoActividadUrl != itemDto.ArchivoActividadUrl)
                    {
                        // Construye la ruta del archivo antiguo en el sistema de archivos
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), existingItem.ArchivoActividadUrl.TrimStart('/'));

                        // Verifica si el archivo antiguo existe y lo elimina
                        if (System.IO.File.Exists(oldFilePath))
                        {

                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                else
                {
                    // Si no se proporciona un nuevo archivo, mantiene la URL del archivo antiguo
                    itemDto.ArchivoActividadUrl = existingItem.ArchivoActividadUrl;
                }

                // Llama al servicio para modificar la actividad con los nuevos datos
                await service.ModifyItem(itemDto);

                // Devuelve una respuesta 200 OK si la modificación se realizó con éxito
                return Ok();
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, devuelve un código de estado 500 (Error Interno del Servidor) con el mensaje de error
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> DeleteActividad([FromRoute] int id)
        {
            await service.DeleteItem(id);
            return Ok();
        }
        [HttpGet("download/{id}")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            // Llama al servicio para obtener el MemoryStream, el tipo de contenido y el nombre del archivo.
            var (fileStream, contentType, fileName) = await service.DownloadFile(id);

            // Verifica si el archivo existe y se pudo obtener.
            if (fileStream == null || fileStream.Length == 0)
            {
                return NotFound("El archivo no se encuentra disponible.");
            }

            // Reajusta la posición del MemoryStream a 0 para que pueda ser leído desde el principio.
            fileStream.Position = 0;

            // Devuelve el archivo como una respuesta de descarga.
            return File(fileStream, contentType, fileName);
        }

        [HttpGet("ActividadesUsuarioId")]
        public async Task<ActionResult<Actividad>> GetActividadByUsuarioId()
        {
            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("Unable to find user ID with the given token");
                var actividades = await service.GetActividadUsuarioID(UserId);
                return Ok(actividades);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
