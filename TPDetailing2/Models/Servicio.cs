using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPDetailing2.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(25, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(1, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(4500, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string Descripcion { get; set; }
        
        [DisplayName("Foto")]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(500, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string? FotoUrl { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Range(0,1000000, ErrorMessage = ErrorViewModel.PrecioValido)]
        public int? Costo { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [Range(0,1000000, ErrorMessage = ErrorViewModel.PrecioValido)]
        public int? PrecioFinal { get; set; }

    }
}