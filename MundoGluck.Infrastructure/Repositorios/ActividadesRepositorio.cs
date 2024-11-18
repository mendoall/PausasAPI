using Microsoft.EntityFrameworkCore;
using MundoGluck.Domain.Entidades;
using MundoGluck.Infrastructure.Data;
using MundoGluck.Domain.Repositorios;
using MundoGluck.Domain.Exceptions;
using System.Data;


namespace MundoGluck.Infrastructure.Repositorios
{

    internal class ActividadesRepositorio(MundoGluck_DbContext dbContext) : IActividadRepositorio
    {
        
        public async Task<IEnumerable<Actividad>> GetAllAsync()       {
            
            return await dbContext.Actividades
                .Include(a => a.Modulo)
                .Include(a => a.Usuario)
                .ToListAsync();
        }

        public async Task<Actividad?> GetItemByIdAsync(int id)
        {
            return await dbContext.Actividades.FirstOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<int> AddItemAsync(Actividad item)
        {
            item.Modulo = await dbContext.Modulos.FirstOrDefaultAsync(x => x.Id == item.ModuloId)
                ?? throw new NotFoundException(nameof(Empresa), item.ModuloId.ToString());
              dbContext.Actividades.Add(item);
            await dbContext.SaveChangesAsync();

            return item.Id; 
        }

        public async Task ModifyItemAsync(Actividad item)
        {
            //TODO Si el modulo es requerido, esto deberia setearlo el servicio. Porque requerido para el update
            item.Modulo = await dbContext.Modulos.FirstOrDefaultAsync(x => x.Id == item.ModuloId)
                ?? throw new NotFoundException(nameof(Empresa), item.ModuloId.ToString());
            dbContext.Actividades.Update(item);
            await dbContext.SaveChangesAsync();
        }

        
        public async Task DeleteItemAsync(int id)
        {
            var item = await dbContext.Actividades.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException(nameof(Actividad), id.ToString());
            dbContext.Actividades.Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Actividad>> GetActivididadByUsuarioIdAsync(string id)
        {
            return await dbContext.Actividades.Where(x => x.UsuarioId == id).ToListAsync();
        }
    }
}
