namespace Aplicacion.Dto.DatosMaestros.Request
{
    public class ModalidadRequest
    {
        public int IdModalidad { get; set; }
        public string NombreModalidad { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
