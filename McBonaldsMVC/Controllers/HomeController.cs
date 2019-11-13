using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using McBonaldsMVC.Models;

namespace McBonaldsMVC.Controllers
{
    public class HomeController : Controller // Controller é responsavel por receber as informações e retornar o caminho correto, de acordo com o que o usuário solicitou 
    {
        public IActionResult Index()
        {
            ViewData["NomeView"] = "Home"; //ViewData é uma variável genérica como um dicionário..
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
