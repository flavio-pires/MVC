using System;
using McBonaldsMVC.Models;
using McBonaldsMVC.Repositories;
using McBonaldsMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class PedidoController : Controller
    {
        PedidoRepository pedidoRepository = new PedidoRepository ();
        HamburguerRepository hamburguerRepository = new HamburguerRepository();
        ShakeRepository shakeRepository = new ShakeRepository();

        public IActionResult Index () {

            PedidoViewModel pedido = new PedidoViewModel();
            pedido.Hamburgueres = hamburguerRepository.ObterTodos();
            pedido.Shakes = shakeRepository.ObterTodos();

            return View (pedido);
        }

        public IActionResult Registrar (IFormCollection form) {
            ViewData["Action"] = "Pedido";
            Pedido pedido = new Pedido ();

            Shake shake = new Shake ();
            var nomeShake = form["shake"];
            shake.Nome = nomeShake;
            shake.Preco = shakeRepository.ObterPrecoDe(nomeShake);

            pedido.Shake = shake;

            var nomeHamburguer = form["hamburguer"];
            Hamburguer hamburguer = new Hamburguer (nomeHamburguer, hamburguerRepository.ObterPrecoDe(nomeHamburguer));

            pedido.Hamburguer = hamburguer;

            Cliente cliente = new Cliente () {
                Nome = form["nome"],
                Endereco = form["endereco"],
                Telefone = form["telefone"],
                Email = form["email"]
            };

            pedido.Cliente = cliente;

            pedido.DataDoPedido = DateTime.Now;

            pedido.Precototal = hamburguer.Preco + shake.Preco;

            if (pedidoRepository.Inserir (pedido)) {
                return View ("Sucesso");
            } else {
                return View ("Erro");
            }
        }
    }
}