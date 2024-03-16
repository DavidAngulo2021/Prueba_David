using System.ComponentModel.DataAnnotations;

namespace Cliente.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidateFechaNacimiento(ErrorMessage = "La fecha de nacimiento no puede ser posterior a hoy.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sueldo es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0.")]
        public decimal Sueldo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class ValidateFechaNacimientoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime fechaNacimiento = (DateTime)value;
            if (fechaNacimiento > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
