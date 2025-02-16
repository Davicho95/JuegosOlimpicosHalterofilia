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
    public class ModalidadesController : BaseApiController
    {
        private readonly IModalidadesRepositoryAsync _modalidad;

        public ModalidadesController(IModalidadesRepositoryAsync modalidad)
        {
            _modalidad = modalidad;
        }

        [HttpGet("ObtenerModalidades")]
        public async Task<IActionResult> ObtenerModalidades([FromQuery] int? idModelidad)
        {
            Response<List<Modalidad>> response = await _modalidad.ObtenerModalidades(idModelidad);
            return Ok(response);
        }

        [HttpPost("CrearModalidad")]
        public async Task<IActionResult> CrearModalidad(ModalidadRequest modalidad)
        {
            Response<Modalidad> response = await _modalidad.CrearModalidad(modalidad);
            return Ok(response);
        }

        [HttpPut("ActualizarModalidad")]
        public async Task<IActionResult> ActualizarModalidad(ModalidadRequest modalidad)
        {
            Response<Modalidad> response = await _modalidad.ActualizarModalidad(modalidad);
            return Ok(response);
        }
    }
}
