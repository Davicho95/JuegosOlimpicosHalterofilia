using Aplicacion.Dto.Seguridades.Request;
using Aplicacion.Wrappers;

namespace Aplicacion.Interfaces
{
    public interface ISeguridades
    {
        Task<Response<string>> GenerarToken(UsuarioRequest request);
    }
}
