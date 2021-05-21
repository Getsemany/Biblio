using Microsoft.EntityFrameworkCore;
using Biblio.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Biblio.Data
{
    public class LibrosContext : IdentityDbContext<IdentityUser>
    {
        public LibrosContext (DbContextOptions<LibrosContext> options)
            : base(options)
        {
        }

        public DbSet<Libros> Libros { get; set; }
        public DbSet<Biblio.Models.Contact> Contact { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}