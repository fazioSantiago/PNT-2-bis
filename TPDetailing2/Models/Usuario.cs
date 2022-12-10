using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPDetailing2.Models
{
    public class Usuario
    {
        [Key]
        [DisplayName("Cliente ID")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3,ErrorMessage =ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30,ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(3, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(25, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string? Email { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(1, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(15, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string? Telefono { get; set; }


    }
}
