using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Login"
            });
        }
    }
}