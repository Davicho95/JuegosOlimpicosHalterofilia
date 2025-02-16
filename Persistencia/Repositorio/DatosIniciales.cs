using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class DatosIniciales : IDatosIniciales
    {
        private readonly AppDbContext _context;

        public DatosIniciales(AppDbContext context)
        {
            _context = context;
        }

        public void CreacionBaseDeDatos(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    if (!context.Database.CanConnect())
                    {
                        Console.WriteLine("La base de datos no existe, se procederá a crearla.");
                        context.Database.EnsureCreated();
                    }

                    context.Database.Migrate();
                    Console.WriteLine("Base de datos actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
                }
            }
        }

        public async Task InsetarDatosIniciales()
        {
            try
            {
                if (!_context.Usuarios.Any())
                {
                    _context.Usuarios.Add(new()
                    {
                        NombreUsuario = "Admin",
                        Contrasenia = "Admin",
                        FechaCreacion = DateTime.Now,
                        Activo = true
                    });
                }

                if (!_context.Paises.Any())
                {
                    _context.Paises.AddRange(new List<Pais>
                    {
                        new Pais { CodigoPais = "USA", NombrePais = "Estados Unidos", Activo = true },
                        new Pais { CodigoPais = "MEX", NombrePais = "México", Activo = true },
                        new Pais { CodigoPais = "COL", NombrePais = "Colombia", Activo = true }
                    });
                }

                if (!_context.Modalidades.Any())
                {
                    _context.Modalidades.AddRange(new List<Modalidad>
                    {
                        new Modalidad { NombreModalidad = "Arranque", EsArranque= true, EsEnvion =false, Activo = true },
                        new Modalidad { NombreModalidad = "Envión", EsArranque = false, EsEnvion = true, Activo = true },
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException($"No se pudo insertar los datos iniciales en la Base de datos: {ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}
