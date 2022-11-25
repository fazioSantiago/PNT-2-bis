namespace TPDetailing2.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        //Errores personalizados
        public const string CampoRequerido = "{0} es obligatorio";
        public const string CaracteresMinimos = "{0} no debe exceder los {1} caracteres.";
        public const string CaracteresMaximos = "{0} no debe superar los {1} caracteres.";
        public const string PrecioValido = "{0} debe ser mayor que ${1} y menor a ${2}.";
    }
}