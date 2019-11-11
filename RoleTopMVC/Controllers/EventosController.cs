using Microsoft.AspNetCore.Mvc;

namespace RoleTopMVC.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Shows()
        {
            return View();
        }
    }
}