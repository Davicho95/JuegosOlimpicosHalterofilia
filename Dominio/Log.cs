using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Log
    {
        [Key]
        public int IdLog { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoLog { get; set; } = null!;  //Puede ser: Error o Ok

        [Required]
        [StringLength(1000)]
        public string Mensaje { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string? Json { get; set; } = null!;

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
