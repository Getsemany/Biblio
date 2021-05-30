using System;
using System.ComponentModel.DataAnnotations;

namespace Biblio.Models
{
    public class Prestamos
    {
        [Required]
        public int ID{get;set;}
        [Required]
        [Display(Name = "Nombre del alumno")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [StringLength(30)]
        public string nombre{get;set;}
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Display(Name = "Titulo del libro")]
        [StringLength(40)]
        public string nombreLibro{get;set;}
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de prestamo")]
        public DateTime fechaPrestamo{get;set;}
        [Required]
        [Display(Name = "Estado del prestamo")]
        public bool estado{get;set;}
    }
}