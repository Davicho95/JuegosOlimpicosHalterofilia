using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Wrappers;
using Dominio;

namespace Aplicacion.Interfaces
{
    public interface IModalidadesRepositoryAsync
    {
        Task<Response<List<Modalidad>>> ObtenerModalidades(int? idModalidad);

        Task<Response<Modalidad>> CrearModalidad(ModalidadRequest request);

        Task<Response<Modalidad>> ActualizarModalidad(ModalidadRequest request);
    }
}
