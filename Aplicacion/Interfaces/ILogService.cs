using Dominio;

namespace Aplicacion.Interfaces
{
    public interface ILogService
    {
        Task<int> Log(Log log);
    }
}
