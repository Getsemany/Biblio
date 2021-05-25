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
        public async Task<IActionResult> Prestar(int? id){
        var libros = await _context.Libros.FindAsync(id);
        if(libros.Cantidad>0){
            libros.Cantidad--;
        _context.Libros.Update(libros);
        }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
        public async Task<IActionResult> Devolver(int? id){
        var libros = await _context.Libros.FindAsync(id);
        libros.Cantidad++;
        _context.Libros.Update(libros);
        
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
    }
}
