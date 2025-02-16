using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class Modalidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModalidad { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreModalidad { get; set; } = null!;

        [Required]
        public bool EsArranque { get; set; }

        [Required]
        public bool EsEnvion { get; set; }

        [Required]
        public bool Activo { get; set; }


        [JsonIgnore]
        public virtual ICollection<Intento> Intentos { get; set; } = new List<Intento>();
    }
}
