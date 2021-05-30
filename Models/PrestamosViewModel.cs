using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Biblio.Models;
namespace Biblio.Models
{
   public class PrestamosViewModel{
        public List<Prestamos> Prestamos { get; set; }
        
        public SelectList titulo { get; set; }
        
        public Prestamos prestamo{get;set;}
    }
}