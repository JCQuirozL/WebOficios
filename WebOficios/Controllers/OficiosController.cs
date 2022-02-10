using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _enviroment;

        public OficiosController(oficiosContext context, IWebHostEnvironment env)
        {
            _context = context;
            _enviroment = env;  
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        // GET: OficiosController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Listado(int id)
        {
            var lst = (from Oficios in _context.Oficios
                       join TipoOficios in _context.TipoOficios on Oficios.IdTipo equals TipoOficios.IdTipo
                       join Direcciones in _context.Direcciones on Oficios.IdDireccion equals Direcciones.IdDireccion
                       select new
                       {
                           idOficio = Oficios.IdOficio,
                           numOficio = Oficios.NOficio,
                           fecha = Oficios.Fecha,
                           tipoOficio = TipoOficios.Nombre,
                           asunto = Oficios.Asunto,
                           nombreDireccion = Direcciones.Nombre,
                           folioSolicitud = Oficios.FolioSolicitud,
                           usuario=Oficios.Capturo


                       }).ToList();

            return  Json (new  {  data = lst });
        }

        [Authorize]
        [HttpGet]
        // GET: OficiosController/Create
        public async Task<ActionResult> Create(int? id)
        {
            Oficio Oficio = new();
            ViewBag.Tipos = _context.TipoOficios.Select(t => new { IdTipo = t.IdTipo, Nombre = t.Nombre}).ToList();

            ViewBag.Direcciones = _context.Direcciones.Select(d => new { IdDireccion = d.IdDireccion, Nombre = d.Nombre}).ToList();

            

            if (id == null)
            {

                return View(Oficio);
            }
            else
            {
                
                Oficio = await _context.Oficios.FindAsync(Convert.ToInt64(id));
                return View(Oficio);
            }
        }

        // POST: OficiosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Oficio oficio )
        {

            

            ViewBag.Tipos = _context.TipoOficios.Select(t => new { IdTipo = t.IdTipo, Nombre = t.Nombre }).ToList();

            ViewBag.Direcciones = _context.Direcciones.Select(d => new { IdDireccion = d.IdDireccion, Nombre = d.Nombre }).ToList();

           
            if (ModelState.IsValid)
            {
                

                if (oficio.IdOficio == 0) //Crear registro
                {
                   

                        using (var ms = new MemoryStream())
                        {
                            await oficio.FormFile.CopyToAsync(ms);


                            oficio.PdfArchivo = ms.ToArray();
                            
                                
                        };
                    

                    await _context.Oficios.AddAsync(oficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 }); //Lo añadí para que al momento de añadir uno nuevo no salte el modal del último registro
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        await oficio.FormFile.CopyToAsync(ms);


                        oficio.PdfArchivo = ms.ToArray();


                    };


                    _context.Oficios.Update(oficio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create), new { id = 0 });
                }
            }
           

            return View(oficio);
        }



        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var oficio = await _context.Oficios.FindAsync(Convert.ToInt64(id));
            if (oficio == null)
            {
                return Json(new { success = false, message = "No se pudo borrar el registro" });
            }

            _context.Oficios.Remove(oficio);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Oficio borrado con éxito" });
        }

      [Route("~/OnPostDownLoadAsync/{id}")]
        public async Task<ActionResult> OnPostDownLoadAsync(int id)
        {

            using (var db = new oficiosContext()) 
            { 
                var miOficio = await db.Oficios.FindAsync(Convert.ToInt64(id));
                return File(miOficio.PdfArchivo, "application/pdf", fileDownloadName: $"Oficio número {miOficio.NOficio}.pdf");
            }
            
        }


    }
}