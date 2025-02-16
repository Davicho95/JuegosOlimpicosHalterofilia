namespace Aplicacion.Dto.DatosMaestros.Request
{
    public class PaisRequest
    {
        public string CodigoPais { get; set; } = string.Empty;

        public string NombrePais { get; set; } = string.Empty;

        public bool Activo { get; set; }
    }
}
