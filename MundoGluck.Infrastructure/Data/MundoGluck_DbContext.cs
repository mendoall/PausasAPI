
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MundoGluck.Domain.Entidades;
namespace MundoGluck.Infrastructure.Data
{
    public class MundoGluck_DbContext : IdentityDbContext<Usuario>
    {
        public MundoGluck_DbContext(DbContextOptions<MundoGluck_DbContext> options) : base(options)
        {

        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Amigo> Amigos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Insignia> Insignias { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Progreso> Progresos { get; set; }
        public DbSet<Reporte> Reportes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Modulo>()
                .HasMany(m => m.actividades)
                .WithOne(a => a.Modulo)
                .HasForeignKey(a => a.ModuloId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                 .HasOne(u => u.Empresa)
                 .WithMany()
                 .HasForeignKey(u => u.EmpresaId);

            modelBuilder.Entity<Usuario>()
                 .HasMany(u => u.Actividades)
                 .WithOne(a => a.Usuario)
                 .HasForeignKey(a => a.UsuarioId);


        }
    }
}
