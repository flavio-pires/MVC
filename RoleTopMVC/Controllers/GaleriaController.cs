using Microsoft.AspNetCore.Mvc;

namespace RoleTopMVC.Controllers
{
    public class GaleriaController : Controller
    {
        public IActionResult Fotos()
        {
            return View();
        }
    }
}