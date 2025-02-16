using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Dominio
{
    public class Pais
    {
        [Key]
        public string CodigoPais { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombrePais { get; set; } = string.Empty;

        [Required]
        public bool Activo { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Deportista> Deportistas { get; set; } = new List<Deportista>();
    }
}
