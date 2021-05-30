using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblio.Data;
using Biblio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Biblio.Authorization;

namespace Biblio.Controllers
{
    
    public class LibrosController : Controller
    {
        private readonly LibrosContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly    UserManager<IdentityUser> _userManager;

        public LibrosController(LibrosContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {   
            _context = context;
            _authorizationService=authorizationService;
             _userManager=userManager;
        }

        // GET: Libros
        [AllowAnonymous]
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Libros
                                    orderby m.Genero
                                    select m.Genero;

            var libros = from l in _context.Libros
                 select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                libros = libros.Where(s => s.Titulo.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(movieGenre))
            {
                libros = libros.Where(x => x.Genero == movieGenre);
            }
             
            var libros1 = new LibrosViewModel
            {
                Genero = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Libros = await libros.ToListAsync()
            };

            return View(libros1);
        }

        // GET: Libros/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // GET: Libros/Create
        public async Task<IActionResult> Create()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Fecha,Autor,Genero,Cantidad")] Libros libros)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
            return Forbid();
            }
            if (ModelState.IsValid)
            {
                _context.Add(libros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libros);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }
            return View(libros);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Fecha,Autor,Genero,Cantidad")] Libros libros)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            if (id != libros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrosExists(libros.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libros);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
           var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            if (id == null)
            {
                return NotFound();
            }

            var libros = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libros == null)
            {
                return NotFound();
            }

            return View(libros);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            var libros = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrosExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Prestar(){
             var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            IQueryable<string> libroQuery = from m in _context.Libros
                                    orderby m.Titulo
                                    select m.Titulo;

            var titulos = new PrestamosViewModel
            {
                titulo = new SelectList(await libroQuery.Distinct().ToListAsync())
            };
            return View(titulos);
        }
        [HttpPost]
        public async Task<IActionResult> Prestar( PrestamosViewModel pr){
             var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            var libros = from l in _context.Libros
                 select l;
            libros = libros.Where(x => x.Titulo.Contains(pr.prestamo.nombreLibro));
            if(libros==null)
            {
                return NotFound();
            }
            if(libros.First().Cantidad>0)
            {
                libros.First().Cantidad--;
                _context.Libros.Update(libros.First());
                pr.prestamo.estado=true;
                _context.Prestamos.Add(pr.prestamo);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
        public async Task<IActionResult> Devolver(){
             var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            var prestamos = from l in _context.Prestamos
                 select l;
            prestamos=prestamos.Where(x=> x.estado==true);
            var pres=new PrestamosViewModel
            {
                Prestamos=await prestamos.ToListAsync()
            };
            return View(pres);
        }

        public async Task<IActionResult> Devolver2(int id){
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, _userManager.GetUserId(User),
                                                ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            
            var prestamo = await _context.Prestamos.FindAsync(id);
            var libros = from l in _context.Libros
                 select l;
            libros = libros.Where(x => x.Titulo.Contains(prestamo.nombreLibro));
            libros.First().Cantidad++;
            prestamo.estado=false;
            _context.Prestamos.Update(prestamo);
            _context.Libros.Update(libros.First());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
