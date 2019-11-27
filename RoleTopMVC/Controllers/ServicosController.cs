using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class ServicosController : Controller
    {
        public IActionResult Trabalhos()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Servicos"
            });
        }
    }
}