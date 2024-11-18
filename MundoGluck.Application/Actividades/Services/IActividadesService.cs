using MundoGluck.Application.Actividades.DTOs;
using MundoGluck.Domain.Entidades;

namespace MundoGluck.Application.Actividades.Services;

public interface IActividadesService
{
    Task<IEnumerable<ActividadDTO>> GetAllItems();
    Task<Actividad?> GetByID(int id);
    Task ModifyItem(ActividadDTO item);
    Task<int> AddItem(ActividadDTO item);
    Task DeleteItem(int id);
    Task<(MemoryStream FileStream, string ContentType, string FileName)> DownloadFile(int id);
    Task <List<Actividad?>> GetActividadUsuarioID(string id);

}