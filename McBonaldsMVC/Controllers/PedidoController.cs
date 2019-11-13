using McBonaldsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar(IFormCollection form)
        {
            Pedido pedido = new Pedido();

            Shake shake = new Shake();
            shake.Nome = form["shake"];
            shake.Preco = 0.0;

            return View("Sucesso");
        }
    }
}