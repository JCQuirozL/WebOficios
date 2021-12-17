using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOficios.Data;
using WebOficios.Models;

namespace WebOficios.Controllers
{
    public class OficiosController : Controller
    {
        private readonly oficiosContext _context;

        public OficiosController(oficiosContext context)
        {
            _context = context;
        }
        // GET: OficiosController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Listado(int id)
        {
            var lst = await _context.Oficios.ToListAsync();
            return Json(new { data = lst });
        }

        // GET: OficiosController/Create
        public async Task<ActionResult> Create(int? id)
        {
            Oficio Oficio = new Oficio();
            ViewBag.Tipos = _context.TipoOficios.Select(t => new { IdTipo = t.IdTipo, Nombre = t.Nombre }).ToList();
            ViewBag.Direcciones = _context.Direcciones.Select(d => new { IdDireccion = d.IdDireccion, Nombre = d.Nombre }).ToList();
            if (id == null)
            {
                return View(Oficio);
            }
            else
            {
                Oficio = await _context.Oficios.FindAsync(id);
                return View(Oficio);
            }
        }

        // POST: OficiosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Oficio oficio)
        {
            

            if (ModelState.IsValid)
            {
                if (oficio.IdOficio == 0) //Crear registro
                {
                    await _context.Oficios.AddAsync(oficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.Oficios.Update(oficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }

            return View(oficio);
        }

        // GET: OficiosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OficiosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var oficio = await _context.Oficios.FindAsync(id);
            if (oficio == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.Oficios.Remove(oficio);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Oficio borrado con éxito" });
        }

      
    }
}
