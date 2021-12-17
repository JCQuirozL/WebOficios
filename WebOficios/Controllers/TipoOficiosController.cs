using Microsoft.AspNetCore.Mvc;

namespace WebOficios.Controllers
{
    public class TipoOficiosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int? id )
        {

        }
    }
}
