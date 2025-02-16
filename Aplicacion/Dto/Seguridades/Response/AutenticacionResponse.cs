namespace Aplicacion.Dto.Seguridades.Response
{
    public class AutenticacionResponse
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string JWToken { get; set; } = string.Empty;
    }
}
