
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Biblio.Data;
using Biblio.Models;
using Biblio.Authorization;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -outDir Pages\Contacts --referenceScriptLibraries
namespace ContactManager.Data
{
    public static class SeedData
    {
        #region snippet_Initialize
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new LibrosContext(
                serviceProvider.GetRequiredService<DbContextOptions<LibrosContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                Seed(serviceProvider);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if(user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }
            
            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new LibrosContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibrosContext>>()))
            {
                // Look for any movies.
                if (context.Libros.Any())
                {
                    return;   // DB has been seeded
                }

                context.Libros.AddRange(
                    new Libros
                    {
                        Titulo = "LA LUZ EN LA OSCURIDAD",
                        Autor="HEINO FALCKE",
                        Fecha = DateTime.Parse("2020-10-04"),
                        Genero = "Ciencia Ficcion",
                        Cantidad = 15
                    },
                    new Libros {
                        Titulo = "Harry Potter y la piedra filosofal",
                        Autor = "J. K. Rowling",
                        Fecha = DateTime.Parse("1997-06-26"),
                        Genero = "Fantasia",
                        Cantidad = 34
                    },

                    new Libros {
                        Titulo = "El Hobbit",
                        Autor = "J. R. R. Tolkien",
                        Fecha = DateTime.Parse("1937-09-21"),
                        Genero = "Fantasia",
                        Cantidad = 20
                    },

                    new Libros {
                        Titulo = "El diario de Ana Frank",
                        Autor = "Ana Frank",
                        Fecha = DateTime.Parse("1947-06-25"),
                        Genero = "Autobiografia",
                        Cantidad = 17
                    },

                    new Libros {
                        Titulo = "La teoria del todo: El origen y el destino del universo",
                        Autor = "Stephen Hawking",
                        Fecha = DateTime.Parse("2010-09-30"),
                        Genero = "Ciencia",
                        Cantidad = 10
                    },

                    new Libros {
                        Titulo = "Relatos de un asesino",
                        Autor = "Donnefar Skedar",
                        Fecha = DateTime.Parse("2020-12-14"),
                        Genero = "Ficción",
                        Cantidad = 5
                    },

                    new Libros {
                        Titulo = "Un mundo muy loco",
                        Autor = "Miguel Alberto Doménech",
                        Fecha = DateTime.Parse("2021-05-14"),
                        Genero = "Literaria",
                        Cantidad = 4
                    },

                    new Libros {
                        Titulo = "El arte de la guerra",
                        Autor = "Sun Tzu",
                        Fecha = DateTime.Parse("2016-12-09"),
                        Genero = "Economia",
                        Cantidad = 5
                    },

                    new Libros {
                        Titulo = "El resplandor",
                        Autor = "Stephen King",
                        Fecha = DateTime.Parse("2012-05-10"),
                        Genero = "Terror",
                        Cantidad = 10
                    },

                    new Libros {
                        Titulo = "1984",
                        Autor = "George Orwell",
                        Fecha = DateTime.Parse("2013-03-07"),
                        Genero = "Ficción",
                        Cantidad = 6
                    },

                    new Libros {
                        Titulo = "It",
                        Autor = "Stephen King",
                        Fecha = DateTime.Parse("2012-11-15"),
                        Genero = "Terror",
                        Cantidad = 19
                    }
                );
                context.SaveChanges();
            }
        }
        
    }
}