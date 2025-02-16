using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class PaisRepositryAsync : IPaisRepositryAsync
    {
        private readonly AppDbContext _context;

        public PaisRepositryAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<Pais>>> ObtenerPaises(string? codigoPais)
        {
            try
            {
                var query = from pais in _context.Paises select pais;
                if (!string.IsNullOrEmpty(codigoPais))
                {
                    query = query.Where(x => x.CodigoPais.Equals(codigoPais));
                }

                return new Response<List<Pais>>(await query.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al recuperar los paises: {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Response<Pais>> CrearPais(PaisRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();

                if (string.IsNullOrEmpty(request.CodigoPais))
                {
                    errores.Add("El campo CodigoPais es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.NombrePais))
                {
                    errores.Add("El campo NombrePais es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                Pais pais = new()
                {
                    NombrePais = request.NombrePais,
                    CodigoPais = request.CodigoPais,
                    Activo = true
                };

                _context.Paises.Add(pais);

                await _context.SaveChangesAsync();

                return new Response<Pais>(pais, "País creado correctamente");
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al ingresar un país: {ex.Message}", ex.InnerException);
            }
        }
        public async Task<Response<Pais>> ActualizarPais(PaisRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();

                if (string.IsNullOrEmpty(request.CodigoPais))
                {
                    errores.Add("El campo CodigoPais es obligatorio.");
                }
                if (string.IsNullOrEmpty(request.NombrePais))
                {
                    errores.Add("El campo NombrePais es obligatorio.");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion



                Pais pais = _context.Paises.FirstOrDefault(x=>x.CodigoPais.Equals(request.CodigoPais)) ?? throw new ApiException($"No existe un país ingresado con código: {request.CodigoPais}");

                pais.NombrePais = request.NombrePais;
                pais.Activo = request.Activo;

                await _context.SaveChangesAsync();

                return new Response<Pais>(pais, "País actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Error al ingresar un país: {ex.Message}", ex.InnerException);
            }
        }
    }
}
