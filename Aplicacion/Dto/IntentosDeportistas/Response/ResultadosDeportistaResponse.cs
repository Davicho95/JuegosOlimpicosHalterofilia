namespace Aplicacion.Dto.IntentosDeportistas.Response
{
    public class ResultadosDeportistaResponse
    {
        public string Pais { get; set; } = string.Empty;

        public string NombreDeportista { get; set; } = string.Empty;

        public decimal Arranque { get; set; }

        public decimal Envion { get; set; }

        public decimal TotalPeso { get; set; }
    }
}
