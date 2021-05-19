using Microsoft.EntityFrameworkCore;
using Biblio.Models;

namespace Biblio.Data
{
    public class LibrosContext : DbContext
    {
        public LibrosContext (DbContextOptions<LibrosContext> options)
            : base(options)
        {
        }

        public DbSet<Libros> Libros { get; set; }
    }
}