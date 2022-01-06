using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOficios.Data;
using WebOficios.Models;

namespace WebOficios.Controllers
{
    public class TipoOficiosController : Controller
    {

        private readonly oficiosContext _context;

        public TipoOficiosController(oficiosContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Create(int? id )
        {
            TipoOficio tOficio = new TipoOficio();
            
            if (id == null)
            {
                return View(tOficio);
            }
            else
            {
                tOficio = await _context.TipoOficios.FindAsync(id);
                return View(tOficio);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoOficio tOficio)
        {

            if (ModelState.IsValid)
            {
                if (tOficio.IdTipo == 0) //Crear registro
                {
                    await _context.TipoOficios.AddAsync(tOficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.TipoOficios.Update(tOficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }

            return View(tOficio);
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var lst = await _context.TipoOficios.ToListAsync();
            return Json(new { data = lst });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var tOficio = await _context.TipoOficios.FindAsync(id);
            if (tOficio == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.TipoOficios.Remove(tOficio);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Registro borrado con éxito" });

        }


    }
}
