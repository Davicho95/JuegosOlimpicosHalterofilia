using Aplicacion.Dto.IntentosDeportistas.Request;
using Aplicacion.Dto.IntentosDeportistas.Response;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace JuegosOlimpicosApi.Controllers
{
    [Authorize]
    public class IntentosDeportistasController : BaseApiController
    {
        private readonly IIntentosDeportistaRepositoryAsync _intentosDeportista;
        private readonly ILogService _logs;

        public IntentosDeportistasController(IIntentosDeportistaRepositoryAsync intentosDeportista, ILogService logs)
        {
            _intentosDeportista = intentosDeportista;
            _logs = logs;
        }

        [HttpGet("ObtenerResultadosDeportistas")]
        public async Task<IActionResult> ObtenerResultadosDeportistas(int numeroPagina, int cantidadResultado)
        {
            Response<List<ResultadosDeportistaResponse>> response = await _intentosDeportista.ObtenerResultadosDeportistas(numeroPagina, cantidadResultado);
            return Ok(response);
        }

        [HttpGet("ObtenerNumeroIntentosDeportistas")]
        public async Task<IActionResult> ObtenerNumeroIntentosDeportistas()
        {
            Response<List<NumeroIntentosDeportistaResponse>> response = await _intentosDeportista.ObtenerNumeroIntentosDeportistas();
            return Ok(response);
        }

        [HttpPost("AgregarIntentoDeportista")]
        public async Task<IActionResult> AgregarIntentoDeportista(IntentoDeportistaRequest request)
        {
            Response<Intento> response = await _intentosDeportista.AgregarIntentoDeportista(request);
            return Ok(response);
        }
    }
}
