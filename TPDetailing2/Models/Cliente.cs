using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPDetailing2.Models
{
    public class Cliente : Usuario
    {
        List<Turno>? turnos = null;
    }
}
