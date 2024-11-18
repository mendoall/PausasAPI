
using Microsoft.Extensions.Logging;
using MundoGluck.Application.Actividades.DTOs;
using MundoGluck.Domain.Entidades;
using MundoGluck.Domain.Exceptions;
using MundoGluck.Domain.Repositorios;
using System;
using System.Windows.Markup;


namespace MundoGluck.Application.Actividades.Services;

internal class ActividadesService( IActividadRepositorio actividadesRepositorio, ILogger<ActividadesService> logger ) : IActividadesService
{
    private const string FolderPath = "Uploads/Actividades";
    public async Task<IEnumerable<ActividadDTO>> GetAllItems()      
    {
        
        logger.LogInformation("Obteniendo todas las actividades");
        var actividades = await actividadesRepositorio.GetAllAsync();
        var actividadesDTO = actividades.Select(a => new ActividadDTO
        {
            Id = a.Id,
            Contenido = a.Contenido,
            FechaCreacion = a.FechaCreacion,
            ModuloId = a.ModuloId,
            UsuarioId = a.UsuarioId,
            ModuloNombre = a.Modulo.Nombre,
            UsuarioEmail = a.Usuario.Email,
            ArchivoActividadUrl= a.ArchivoActividadUrl,
            LinkActividad= a.LinkActividad,
           UsuarioNombre= a.Usuario.Nombre
        }).ToList();

        return actividadesDTO;
    }

    public async Task<Actividad?> GetByID(int id)
    {
        return await actividadesRepositorio.GetItemByIdAsync(id);
    }

    public async Task<int> AddItem(ActividadDTO itemDto)
    {
        var item = ActividadDTO.FromDTO(itemDto)
            ?? throw new Exception("No se pudo convertir el DTO a Entidad");

        // Aquí podrías agregar lógica adicional para validar la URL del archivo si es necesario

        var id = await actividadesRepositorio.AddItemAsync(item);
        return id;
    }

    public async Task ModifyItem(ActividadDTO item)
    {
        var existingItem = await actividadesRepositorio.GetItemByIdAsync(item.Id)
            ?? throw new NotFoundException(nameof(Actividad), item.Id.ToString());

        existingItem.Contenido = item.Contenido;
        //TODO : la fecha de creacion no se puede deberia modificar
        existingItem.FechaCreacion = item.FechaCreacion;
        existingItem.ModuloId = item.ModuloId;
        existingItem.UsuarioId = item.UsuarioId;
        existingItem.ArchivoActividadUrl = item.ArchivoActividadUrl;
        existingItem.LinkActividad = item.LinkActividad;

        await actividadesRepositorio.ModifyItemAsync(existingItem);
    }
    
    public async Task DeleteItem(int id)
    {
        // Paso 1: Obtener la actividad desde el repositorio
        var actividad = await actividadesRepositorio.GetItemByIdAsync(id);

        if (actividad == null)
        {
            throw new ArgumentException("Actividad no encontrada.");
        }

        // Paso 2: Obtener la ruta del archivo desde la base de datos
        var archivoRelativoRuta = actividad.ArchivoActividadUrl;

        // Paso 3: Construir la ruta completa del archivo en el sistema de archivos
        var archivoRuta = Path.Combine(archivoRelativoRuta.TrimStart('/'));

        // Paso 4: Eliminar el archivo del sistema de archivos si existe
        if (File.Exists(archivoRuta))
        {
            File.Delete(archivoRuta);
        }
        else
        {
            // Opcional: Manejar el caso en que el archivo no se encuentre
            // Log o lanzar una excepción si es necesario
        }

        // Paso 5: Eliminar la actividad de la base de datos
        await actividadesRepositorio.DeleteItemAsync(id);
    }
    public async Task<(MemoryStream FileStream, string ContentType, string FileName)> DownloadFile(int id)
    {
        // Busca la actividad con el ID proporcionado en el repositorio.
        var actividad = await actividadesRepositorio.GetItemByIdAsync(id);

        // Verifica si la actividad no existe o si el URL del archivo asociado es nulo o vacío.
        if (actividad == null || string.IsNullOrEmpty(actividad.ArchivoActividadUrl))
        {
            
            throw new ArgumentException("Actividad no encontrada.");
        }

        // Obtiene la ruta del archivo desde la propiedad ArchivoActividadUrl de la actividad.
        var archivoRelativoRuta = actividad.ArchivoActividadUrl;

        
        var path = Path.Combine(archivoRelativoRuta.TrimStart('/'));

        // Verifica si el archivo no existe en la ruta especificada.
        if (!File.Exists(path))
        {
           
            throw new ArgumentException("Archivo no encontrado.");
        }

        // Crea un nuevo MemoryStream para almacenar el contenido del archivo.
        var memory = new MemoryStream();

        // Abre un flujo de archivo para leer el archivo desde el sistema de archivos.
        using (var stream = new FileStream(path, FileMode.Open))
        {
            // Copia el contenido del flujo del archivo al MemoryStream.
            await stream.CopyToAsync(memory);
        }

        // Reajusta la posición del MemoryStream a 0 para que se pueda leer desde el principio.
        memory.Position = 0;

        // Obtiene el nombre del archivo desde la ruta completa del archivo.
        var fileName = Path.GetFileName(path);

        // Detectar el tipo de contenido según la extensión del archivo
        var contentType = "application/octet-stream"; // Tipo de contenido predeterminado para archivos binarios.
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant(); // Obtiene la extensión del archivo en minúsculas.
        switch (fileExtension)
        {
            case ".pdf":
                contentType = "application/pdf";
                break;
           
            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;
            
            case ".png":
                contentType = "image/png";
                break;
        
            case ".gif":
                contentType = "image/gif";
                break;
          
            case ".doc":
            case ".docx":
                contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                break;
                // Se pueden agregar más casos según sea necesario para otros tipos de archivos
        }

        
        // el tipo de contenido (ContentType), y el nombre del archivo (FileName).
        return (memory, contentType, fileName);
    }
    public async Task <List<Actividad?>> GetActividadUsuarioID(string id)
    {
        return await actividadesRepositorio.GetActivididadByUsuarioIdAsync(id);
    }


}

