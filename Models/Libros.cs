using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblio.Models
{
    public class Libros
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Titulo{get;set;}

        [Display(Name = "Fecha de lanzamiento")]
        [DataType(DataType.Date)]
        public DateTime Fecha{get;set;}
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
         [StringLength(30)]
        public string Autor{get;set;}
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
         [Required]
         [StringLength(30)]
        public string Genero{get;set;}
        [Range(1, 100)]
        public int Cantidad {get;set;}
        //public string portada{get;set;}
    }
}
