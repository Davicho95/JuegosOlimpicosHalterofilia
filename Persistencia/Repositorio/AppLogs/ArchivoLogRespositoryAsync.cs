using Aplicacion.Dto.Logs;
using Aplicacion.Interfaces;
using Dominio;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Persistencia.Repositorio.Logs
{
    public class ArchivoLogRespositoryAsync : ILogService
    {
        private readonly IOptions<ConfiguracionLogs> _configLogs;

        public ArchivoLogRespositoryAsync(IOptions<ConfiguracionLogs> configLogs)
        {
            _configLogs = configLogs;
        }

        public async Task<int> Log(Log log)
        {
            try
            {
                // Obtener la ruta base de la aplicación publicada
                var basePath = AppContext.BaseDirectory;

                // Construir la ruta final de la carpeta "logs"
                var logDirectory = Path.Combine(basePath, "Logs");

                // Crear la carpeta "logs" si no existe
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Definir el archivo de logs dentro de la carpeta "logs"
                var _logFilePath = Path.Combine(logDirectory, _configLogs.Value.NombreArchivo);

                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    await writer.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {JsonConvert.SerializeObject(log)}");
                }                
            }
            catch
            {
            }

            return 0;

        }
    }
}
