using Aplicacion.Dto.DatosMaestros.Request;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JuegosOlimpicosApi.Controllers
{
    [Authorize]
    public class PaisController : BaseApiController
    {
        private readonly IPaisRepositryAsync _pais;
        private readonly ILogService _logs;

        public PaisController(IPaisRepositryAsync pais, ILogService logs)
        {
            _pais = pais;
            _logs = logs;
        }

        [HttpGet("ObtenerPaises")]
        public async Task<IActionResult> CrearPais([FromQuery] string? codigoPais)
        {
            Response<List<Pais>> response = await _pais.ObtenerPaises(codigoPais);
            return Ok(response);
        }

        [HttpPost("CrearPais")]
        public async Task<IActionResult> CrearPais(PaisRequest pais)
        {
            Response<Pais> response = await _pais.CrearPais(pais);
            return Ok(response);
        }

        [HttpPut("ActualizarPais")]
        public async Task<IActionResult> ActualizarPais(PaisRequest pais)
        {
            Response<Pais> response = await _pais.ActualizarPais(pais);
            return Ok(response);
        }
    }
}
