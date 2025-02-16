using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class Deportista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDeportista { get; set; }

        [Required]
        [StringLength(15)]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PrimerNombre { get; set; } = string.Empty;

        [StringLength(100)]
        public string? SegundoNombre { get; set; }

        [Required]
        [StringLength(100)]
        public string PrimerApellido { get; set; } = string.Empty;

        [StringLength(100)]
        public string? SegundoApellido { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public bool Activo { get; set; } = true;

        [ForeignKey("Pais")]
        public string CodigoPais { get; set; } = null!;
        [JsonIgnore]
        public virtual Pais PaisNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Intento> Intentos { get; set; } = new List<Intento>();
    }
}
