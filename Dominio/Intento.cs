using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio
{
    public class Intento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdInento { get; set; }

        [ForeignKey("Deportista")]
        public int IdDeportista { get; set; }
        [JsonIgnore]
        public virtual Deportista DeportistaNavigation { get; set; } = null!;

        [Required]
        [ForeignKey("Modalidad")]
        public int IdModalidad { get; set; }
        [JsonIgnore]
        public virtual Modalidad ModalidadNavigation { get; set; } = null!;

        [Required]
        public decimal Peso { get; set; }


    }
}
