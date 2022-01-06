using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebOficios.Data;
using WebOficios.Models;



namespace WebOficios.Controllers
{
    public class AccessController : Controller
    {
        private readonly oficiosContext _context;

        public AccessController(oficiosContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {

            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (oficiosContext db = new oficiosContext())
                {
                    var lst = from d in db.Usuarios
                              where d.Correo == user && d.Password == password && d.Estado=="Activo"
                              select d;
                    if (lst.Count()>0)
                    {
                        Usuario usuario=lst.First();
                        
                        
                    }

                } 
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content("Ocurrió un error: "+ex.Message);
            }
        }
    }
}
