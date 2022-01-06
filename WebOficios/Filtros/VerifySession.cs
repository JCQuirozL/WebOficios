using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using WebOficios.Models;

namespace WebOficios.Filtros
{
    public class VerifySession:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
             
            
            base.OnActionExecuting(filterContext);
        }
    }
}
