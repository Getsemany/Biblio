using System;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblio.Data;
using Biblio.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mime;




namespace Biblio.Controllers
{
    public class ArchivoController : Controller
    {
        private readonly LibrosContext _context;
        private readonly    UserManager<IdentityUser> _userManager;

        public ArchivoController(LibrosContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager=userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(){
            var archivos=from l in _context.Archivo
                 select l;
             archivos = archivos.Where(s => s.IdUsuario.Contains(_userManager.GetUserId(User)));
             var archivos1=new ArchivoViewModel{
                 Archivo = await archivos.ToListAsync()
             };
            return View(archivos1);
        }
        public IActionResult Upload(){

             return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(BufferedSingleFileUploadDbModel fileUploadDb)
        {
        using (var memoryStream = new MemoryStream())
    {
        await fileUploadDb.FileUpload.FormFile.CopyToAsync(memoryStream);

        // Upload the file if less than 2 MB
        if (memoryStream.Length < 2097152)
        {
            var file = new Archivo()
            {
                IdUsuario=_userManager.GetUserId(User),
                Nombre=fileUploadDb.FileUpload.Note,
                UntrustedName=fileUploadDb.FileUpload.FormFile.FileName,
                File = memoryStream.ToArray()
            };

            _context.Archivo.Add(file);

            await _context.SaveChangesAsync();
        }
        else
        {
            ModelState.AddModelError("File", "The file is too large.");
        }
    }

    return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var requestFile = await _context.Archivo.SingleOrDefaultAsync(m => m.ID == id);

            if (requestFile == null)
            {
                return View();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.File, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.UntrustedName));
        }
    }

    
}