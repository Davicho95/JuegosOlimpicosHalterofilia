using Aplicacion.Dto.IntentosDeportistas.Request;
using Aplicacion.Dto.IntentosDeportistas.Response;
using Aplicacion.Wrappers;
using Dominio;

namespace Aplicacion.Interfaces
{
    public interface IIntentosDeportistaRepositoryAsync
    {
        Task<Response<List<ResultadosDeportistaResponse>>> ObtenerResultadosDeportistas(int numeroPagina, int cantidadResultado);

        Task<Response<List<NumeroIntentosDeportistaResponse>>> ObtenerNumeroIntentosDeportistas();

        Task<Response<Intento>> AgregarIntentoDeportista(IntentoDeportistaRequest request);
    }
}
