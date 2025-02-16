using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class ModalidadesRepositoryAsync : IModalidadesRepositoryAsync
    {
        private readonly AppDbContext _appDbContext;

        public ModalidadesRepositoryAsync(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Response<List<Modalidad>>> ObtenerModalidades(int? idModalidad)
        {
            try
            {
                var query = from modalidad in _appDbContext.Modalidades select modalidad;
                if (idModalidad != null)
                {
                    query = query.Where(x => x.IdModalidad == idModalidad);
                }

                return new Response<List<Modalidad>>(await query.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al recuperar las modalidades: {ex.Message}");
            }
        }

        public async Task<Response<Modalidad>> CrearModalidad(ModalidadRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();

                if (string.IsNullOrEmpty(request.NombreModalidad))
                {
                    errores.Add("El campo NombreModalidad es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                Modalidad modalidad = new Modalidad()
                {
                    NombreModalidad = request.NombreModalidad,
                    Activo = true
                };

                _appDbContext.Modalidades.Add(modalidad);

                await _appDbContext.SaveChangesAsync();

                return new Response<Modalidad>(modalidad, "Modalidad creada correctamente");
            }
            catch( ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al crear una modalidad: {ex.Message}");
            }
        }

        public async Task<Response<Modalidad>> ActualizarModalidad(ModalidadRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();

                if (string.IsNullOrEmpty(request.NombreModalidad))
                {
                    errores.Add("El campo NombreModalidad es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                Modalidad modalidad = _appDbContext.Modalidades.FirstOrDefault(x=>x.IdModalidad == request.IdModalidad) ?? throw new ApiException($"No existe una modalidad con ID: {request.IdModalidad}");

                modalidad.NombreModalidad = request.NombreModalidad;
                modalidad.Activo = request.Activo;

                await _appDbContext.SaveChangesAsync();

                return new Response<Modalidad>(modalidad, "Modalidad actualizada correctamente");
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al crear una modalidad: {ex.Message}");
            }
        }
    }
}
