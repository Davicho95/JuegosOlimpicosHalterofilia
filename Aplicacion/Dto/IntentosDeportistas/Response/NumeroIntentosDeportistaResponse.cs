namespace Aplicacion.Dto.IntentosDeportistas.Response
{
    public class NumeroIntentosDeportistaResponse
    {
        public string Pais { get; set; } = string.Empty;

        public string NombreDeportista { get; set; } = string.Empty;

        public decimal IntentosArranque { get; set; }

        public decimal IntentosEnvion { get; set; }

        public decimal TotalIntentos { get; set; }
    }
}
