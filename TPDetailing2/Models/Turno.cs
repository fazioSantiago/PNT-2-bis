using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPDetailing2.Models
{
        public class Turno
    {
        [Key]
        public int TurnoId { get; set; }

        public DateTime? Fecha { get; set; }
        public bool Realizado { get; set; }
        
        public int? ClienteId { get; set; }
        
        [ForeignKey("ClienteId")]
        public Cliente? cliente { get; set; }
                
        public int? ServicioId { get; set; }          
        
        [ForeignKey("ServicioId")]
        public Servicio? Servicio { get; set; }

        public int? EmpleadoId { get; set; }
        
        [ForeignKey ("EmpleadoId")]
        public Empleado? Empleado { get; set; }
        
    }
}