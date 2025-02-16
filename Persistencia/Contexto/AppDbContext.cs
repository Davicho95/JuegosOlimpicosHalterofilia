using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistencia.Contexto
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Deportista> Deportistas { get; set; }
        public DbSet<Modalidad> Modalidades{ get; set; }
        public DbSet<Intento> Intentos { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
