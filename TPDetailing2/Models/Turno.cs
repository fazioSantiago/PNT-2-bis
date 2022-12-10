using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPDetailing2.Models
{
        public class Turno
    {
        [Key]
        public int TurnoId { get; set; }

        public DateTime? Fecha { get; set; }

        [DisplayName("Disponible")]
        public bool Disponible { get; set; } = true;

        [DisplayName("Cliente ID")]
        public int? ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public Cliente? cliente { get; set; }

        [DisplayName("Servicio ID")]
        public int? ServicioId { get; set; }          
        
        [ForeignKey("ServicioId")]
        public Servicio? Servicio { get; set; }

        
    }
}