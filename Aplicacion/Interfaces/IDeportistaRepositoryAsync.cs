using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Wrappers;
using Dominio;

namespace Aplicacion.Interfaces
{
    public interface IDeportistaRepositoryAsync
    {
        Task<Response<List<Deportista>>> ObtenerDeportistas(string? identificacion);

        Task<Response<Deportista>> CrearDeportista(DeportistaRequest request);

        Task<Response<Deportista>> ActualizarDeportista(DeportistaRequest request);
    }
}
