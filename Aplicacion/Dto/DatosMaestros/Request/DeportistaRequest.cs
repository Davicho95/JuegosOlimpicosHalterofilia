namespace Aplicacion.Dto.DatosMaestros.Request
{
    public class DeportistaRequest
    {
        public int IdDeportista { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string PrimerNombre { get; set; } = string.Empty;

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; }

        public int Edad { get; set; }

        public bool Activo { get; set; }

        public string CodigoPais { get; set; } = string.Empty;
    }
}
