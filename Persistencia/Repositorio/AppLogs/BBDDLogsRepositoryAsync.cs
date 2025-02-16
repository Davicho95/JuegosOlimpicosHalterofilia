using Aplicacion.Interfaces;
using Dominio;
using Microsoft.Extensions.DependencyInjection;
using Persistencia.Contexto;

namespace Persistencia.Repositorio.Logs
{
    public class BBDDLogsRepositoryAsync : ILogService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public BBDDLogsRepositoryAsync(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<int> Log(Log log)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Resuelto correctamente en el scope
                    _appDbContext.Logs.Add(log);

                    await _appDbContext.SaveChangesAsync();
                }               
                
            }
            catch
            {
            }
            return log.IdLog;
        }
    }
}
