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
    public class DeportistaController : BaseApiController
    {
        private readonly IDeportistaRepositoryAsync _deportista;

        public DeportistaController(IDeportistaRepositoryAsync deportista)
        {
            _deportista = deportista;
        }

        [HttpGet("ObtenerDeportistas")]
        public async Task<IActionResult> ObtenerDeportistas([FromQuery] string? identificacion)
        {
            Response<List<Deportista>> response = await _deportista.ObtenerDeportistas(identificacion);
            return Ok(response);
        }

        [HttpPost("CrearDeportista")]
        public async Task<IActionResult> CrearDeportista(DeportistaRequest deportista)
        {
            Response<Deportista> response = await _deportista.CrearDeportista(deportista);
            return Ok(response);
        }

        [HttpPut("ActualizarDeportista")]
        public async Task<IActionResult> ActualizarDeportista(DeportistaRequest deportista)
        {
            Response<Deportista> response = await _deportista.ActualizarDeportista(deportista);
            return Ok(response);
        }
    }
}
