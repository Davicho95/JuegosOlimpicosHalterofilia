using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Wrappers;
using Dominio;

namespace Aplicacion.Interfaces
{
    public interface IPaisRepositryAsync
    {
        Task<Response<List<Pais>>> ObtenerPaises(string? codigoPais);
        Task<Response<Pais>> CrearPais(PaisRequest request);
        Task<Response<Pais>> ActualizarPais(PaisRequest request);
    }
}
