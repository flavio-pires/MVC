using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Shows()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Eventos"
            });
        }
    }
}