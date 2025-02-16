using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class DeportistaRepositoryAsync : IDeportistaRepositoryAsync
    {
        private readonly AppDbContext _appDbContext;

        public DeportistaRepositoryAsync(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response<List<Deportista>>> ObtenerDeportistas(string? identificacion)
        {
            try
            {
                var query = from dep in _appDbContext.Deportistas select dep;

                if (!string.IsNullOrEmpty(identificacion))
                {
                    query = query.Where(x => x.Identificacion.Equals(identificacion));
                }

                return new Response<List<Deportista>>(await query.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de recuperar deportistas: {ex.Message}");
            }
        }

        public async Task<Response<Deportista>> CrearDeportista(DeportistaRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();
                if (string.IsNullOrEmpty(request.Identificacion))
                {
                    errores.Add("El campo Identificacion es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.PrimerNombre))
                {
                    errores.Add("El campo PrimerNombre es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.PrimerApellido))
                {
                    errores.Add("El campo PrimerApellido es obligatorio.");
                }
                if (request.Edad == 0)
                {
                    errores.Add("El campo Edad es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.CodigoPais))
                {
                    errores.Add("El campo CodigoPais es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                Deportista deportista = new Deportista()
                {
                    Activo = true,
                    CodigoPais = request.CodigoPais,
                    Edad = request.Edad,
                    Identificacion = request.Identificacion,
                    PrimerApellido = request.PrimerNombre,
                    PrimerNombre = request.PrimerNombre,
                    SegundoApellido = request.SegundoApellido,
                    SegundoNombre = request.SegundoNombre
                };

                _appDbContext.Deportistas.Add(deportista);

                await _appDbContext.SaveChangesAsync();

                return new Response<Deportista>(deportista, "Deportista creado correctamente.");
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de crear al deportista: {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Response<Deportista>> ActualizarDeportista(DeportistaRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();
                if (string.IsNullOrEmpty(request.Identificacion))
                {
                    errores.Add("El campo Identificacion es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.PrimerNombre))
                {
                    errores.Add("El campo PrimerNombre es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.PrimerApellido))
                {
                    errores.Add("El campo PrimerApellido es obligatorio.");
                }
                if (request.Edad == 0)
                {
                    errores.Add("El campo Edad es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.CodigoPais))
                {
                    errores.Add("El campo CodigoPais es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                Deportista deportista = _appDbContext.Deportistas.FirstOrDefault(x => x.IdDeportista == request.IdDeportista) ?? throw new ApiException($"No existe un deportista con ID: {request.IdDeportista}");

                deportista.Identificacion = request.Identificacion;
                deportista.Edad = request.Edad;
                deportista.PrimerNombre = request.PrimerNombre;
                deportista.SegundoNombre = request.SegundoNombre;
                deportista.PrimerApellido = request.PrimerApellido;
                deportista.SegundoApellido = request.SegundoApellido;
                deportista.Activo = request.Activo;
                deportista.CodigoPais = request.CodigoPais;

                await _appDbContext.SaveChangesAsync();

                return new Response<Deportista>(deportista, "Deportista actualizado correctamente.");
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de crear al deportista: {ex.Message}", ex.InnerException);
            }
        }
    }
}
