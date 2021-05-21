using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Biblio.Models
{
    public class LibrosViewModel
    {
        public List<Libros> Libros { get; set; }
        public SelectList Genero { get; set; }
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}