using Aplicacion.Dto.Seguridades.Request;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JuegosOlimpicosApi.Controllers
{
    public class SeguridadesController : BaseApiController
    {
        private readonly ISeguridades _seguridades;
        private readonly ILogService _logs;

        public SeguridadesController(ISeguridades seguridades, ILogService logs)
        {
            _seguridades = seguridades;
            _logs = logs;
        }

        [HttpPost("GenerarToekn")]
        public async Task<IActionResult> GenerarToekn(UsuarioRequest command)
        {
            Response<string> response = await _seguridades.GenerarToken(command);
            return Ok(response);
        }
    }
}
