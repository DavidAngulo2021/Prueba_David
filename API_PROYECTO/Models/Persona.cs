using System;
using System.Collections.Generic;

namespace API_PROYECTO.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public decimal Sueldo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
