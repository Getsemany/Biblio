using System;
using System.ComponentModel.DataAnnotations;

namespace Biblio.Models
{
    public class Libros
    {
        public int Id { get; set; }
        public string Titulo{get;set;}

        [DataType(DataType.Date)]
        public DateTime Fecha{get;set;}

        public string Autor{get;set;}
        public string Genero{get;set;}
        public int Cantidad {get;set;}
        //public string portada{get;set;}
    }
}
