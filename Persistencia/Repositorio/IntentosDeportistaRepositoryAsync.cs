using Aplicacion.Dto.IntentosDeportistas;
using Aplicacion.Dto.IntentosDeportistas.Request;
using Aplicacion.Dto.IntentosDeportistas.Response;
using Aplicacion.Exceptions;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistencia.Contexto;

namespace Persistencia.Repositorio
{
    public class IntentosDeportistaRepositoryAsync : IIntentosDeportistaRepositoryAsync
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOptions<ConfiguracionJuegos> _configuraciones;

        public IntentosDeportistaRepositoryAsync(AppDbContext appDbContext, IOptions<ConfiguracionJuegos> configuraciones)
        {
            _appDbContext = appDbContext;
            _configuraciones = configuraciones;
        }

        public async Task<Response<List<ResultadosDeportistaResponse>>> ObtenerResultadosDeportistas(int numeroPagina, int cantidadResultado)
        {
            try
            {
                if (numeroPagina < 1) numeroPagina = 1;
                if (cantidadResultado < 1) cantidadResultado = 10;

                var totalRegistros = await _appDbContext.Deportistas.CountAsync();

                // Evitar solicitar una página que no existe
                if ((numeroPagina - 1) * cantidadResultado >= totalRegistros && totalRegistros > 0)
                {
                    numeroPagina = (int)Math.Ceiling((double)totalRegistros / cantidadResultado);
                }

                List<ResultadosDeportistaResponse> response = await (from dep in _appDbContext.Deportistas
                                                                     join pais in _appDbContext.Paises on dep.CodigoPais equals pais.CodigoPais
                                                                     join intento in _appDbContext.Intentos on dep.IdDeportista equals intento.IdDeportista
                                                                     join modalidad in _appDbContext.Modalidades on intento.IdModalidad equals modalidad.IdModalidad
                                                                     group new { dep, pais, intento, modalidad } by new { dep.PrimerNombre, dep.SegundoNombre, dep.PrimerApellido, dep.SegundoApellido, pais.NombrePais, pais.CodigoPais } into grupo
                                                                     select new ResultadosDeportistaResponse
                                                                     {
                                                                         Pais = grupo.Key.CodigoPais,
                                                                         NombreDeportista = $"{grupo.Key.PrimerNombre} {grupo.Key.SegundoNombre} {grupo.Key.PrimerApellido} {grupo.Key.SegundoApellido}",
                                                                         Arranque = grupo.Any(x => x.modalidad.EsArranque) ? grupo.Where(x => x.modalidad.EsArranque).Max(x => x.intento.Peso) : 0,
                                                                         Envion = grupo.Any(x => x.modalidad.EsEnvion) ? grupo.Where(x => x.modalidad.EsEnvion).Max(x => x.intento.Peso) : 0,
                                                                         TotalPeso = (grupo.Where(x => x.modalidad.EsArranque).Max(x => (decimal?)x.intento.Peso) ?? 0) +
                                                             (grupo.Where(x => x.modalidad.EsEnvion).Max(x => (decimal?)x.intento.Peso) ?? 0)
                                                                     }).ToListAsync();

                return new Response<List<ResultadosDeportistaResponse>>(response.OrderByDescending(x => x.TotalPeso).Skip((numeroPagina - 1) * (cantidadResultado)).Take(cantidadResultado).ToList());
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de recuperar los resultados de los deportistas: {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Response<List<NumeroIntentosDeportistaResponse>>> ObtenerNumeroIntentosDeportistas()
        {
            try
            {
                List<NumeroIntentosDeportistaResponse> response = await (from dep in _appDbContext.Deportistas
                                                                         join pais in _appDbContext.Paises on dep.CodigoPais equals pais.CodigoPais
                                                                         join intento in _appDbContext.Intentos on dep.IdDeportista equals intento.IdDeportista
                                                                         join modalidad in _appDbContext.Modalidades on intento.IdModalidad equals modalidad.IdModalidad
                                                                         group new { dep, pais, intento, modalidad } by new { dep.PrimerNombre, dep.SegundoNombre, dep.PrimerApellido, dep.SegundoApellido, pais.NombrePais, pais.CodigoPais } into grupo
                                                                         select new NumeroIntentosDeportistaResponse
                                                                         {
                                                                             Pais = grupo.Key.CodigoPais,
                                                                             NombreDeportista = $"{grupo.Key.PrimerNombre} {grupo.Key.SegundoNombre} {grupo.Key.PrimerApellido} {grupo.Key.SegundoApellido}",
                                                                             IntentosArranque = grupo.Where(x => x.modalidad.EsArranque).Count(),
                                                                             IntentosEnvion = grupo.Where(x => x.modalidad.EsEnvion).Count(),
                                                                             TotalIntentos = grupo.Where(x => x.modalidad.EsArranque).Count() + grupo.Where(x => x.modalidad.EsEnvion).Count()
                                                                         }).ToListAsync();

                return new Response<List<NumeroIntentosDeportistaResponse>>(response.ToList());
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de recuperar los intentos de los deportistas: {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Response<Intento>> AgregarIntentoDeportista(IntentoDeportistaRequest request)
        {
            try
            {
                #region Validaciones
                List<string> errores = new();
                if (request.IdDeportista == 0)
                {
                    errores.Add("El campo IdDeportista es obligatorio");
                }
                if (request.IdModalidad == 0)
                {
                    errores.Add("El campo IdModalidad es obligatorio");
                }

                if (errores.Count > 0)
                {
                    throw new ValidationException(errores);
                }
                #endregion

                int intentosRealizados = _appDbContext.Intentos.Where(x => x.IdDeportista == request.IdDeportista && x.IdModalidad == request.IdModalidad).Count();

                if (intentosRealizados >= _configuraciones.Value.MaximoIntentos)
                {
                    throw new ApiException($"El deportista ya ha ingresado el máximo de intentos permitidos. Intentos permitidos = {_configuraciones.Value.MaximoIntentos}");
                }

                Intento intento = new Intento()
                {
                    IdDeportista = request.IdDeportista,
                    IdModalidad = request.IdModalidad,
                    Peso = request.Peso
                };

                _appDbContext.Intentos.Add(intento);

                await _appDbContext.SaveChangesAsync();

                return new Response<Intento>(intento, "Intento creado correctamente.");

            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                throw new ApiException($"Ocurrió un error al momento de agregar un itento: {ex.Message}", ex.InnerException);
            }
        }
    }
}
