using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblio.Data;
using Biblio.Models;

namespace Biblio.Controllers
{
    public class PortadasController : Controller
    {
        private readonly LibrosContext _context;

        public PortadasController(LibrosContext context)
        {
            _context = context;
        }

        // GET: Portadas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Portadas.ToListAsync());
        }

        // GET: Portadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portada = await _context.Portadas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portada == null)
            {
                return NotFound();
            }

            return View(portada);
        }

        // GET: Portadas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Portadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdLibr,portada")] Portada _portada)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(_portada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_portada);
        }

        // GET: Portadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portada = await _context.Portadas.FindAsync(id);
            if (portada == null)
            {
                return NotFound();
            }
            return View(portada);
        }

        // POST: Portadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdLibr,portada")] Portada _portada)
        {
            if (id != _portada.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_portada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortadaExists(_portada.ID))
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
            return View(_portada);
        }

        // GET: Portadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portada = await _context.Portadas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portada == null)
            {
                return NotFound();
            }

            return View(portada);
        }

        // POST: Portadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portada = await _context.Portadas.FindAsync(id);
            _context.Portadas.Remove(portada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortadaExists(int id)
        {
            return _context.Portadas.Any(e => e.ID == id);
        }
    }
}
