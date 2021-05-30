
using System.ComponentModel.DataAnnotations;

namespace Biblio.Models{
    public class Archivo{
        public int ID{get;set;}
        [Required]
        public string IdUsuario{get;set;}
        [Required]
        public byte[] File{get;set;}
        [Required]
        public string Nombre{get;set;}
        public string UntrustedName{get;set;}
    }
}