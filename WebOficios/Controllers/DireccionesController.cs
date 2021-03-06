using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebOficios.Data;
using WebOficios.Models;

namespace WebOficios.Controllers
{
    public class DireccionesController : Controller
    {

        private readonly oficiosContext _context;

        public DireccionesController(oficiosContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int pg = 1)
        {
            
            const int pageSize = 10;

            if(pg < 1)
            {
                pg = 1;
            }

            int recsCount = _context.Direcciones.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;

            List<Direccion> direcciones = _context.Direcciones.Skip(recSkip).Take(pager.PageSize).ToList();

            var totalRegistros = (from d in _context.Direcciones select d).Count();
            this.ViewBag.TotalRegistros = totalRegistros;   
            this.ViewBag.Pager = pager;
            return View(direcciones);
        }
      
        [Authorize]
        public async Task<IActionResult> Create(int? id)
        {
            Direccion direccion = new Direccion();
            
            if (id == null) 
            { 
                return View(direccion);
            }
            else
            {
                direccion = await _context.Direcciones.FindAsync(id);
                return View(direccion);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Direccion direccion)
        {

            if (ModelState.IsValid)
            {
                if(direccion.IdDireccion == 0) //Crear registro
                { 
                    await _context.Direcciones.AddAsync(direccion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    _context.Direcciones.Update(direccion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id=0 });
                }
            }

            return View(direccion);
        }

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            var lst = await _context.Direcciones.ToListAsync();
            return Json(new {data = lst});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Dirección borrada con éxito" });

        }
    }
}
