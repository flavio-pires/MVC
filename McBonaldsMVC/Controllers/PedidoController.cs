using System;
using McBonaldsMVC.Models;
using McBonaldsMVC.Repositories;
using McBonaldsMVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class PedidoController : AbstractController
    {
        PedidoRepository pedidoRepository = new PedidoRepository ();
        HamburguerRepository hamburguerRepository = new HamburguerRepository();
        ShakeRepository shakeRepository = new ShakeRepository();
        ClienteRepository clienteRepository = new ClienteRepository();

        public IActionResult Index () {

            PedidoViewModel pedido = new PedidoViewModel();
            pedido.Hamburgueres = hamburguerRepository.ObterTodos();
            pedido.Shakes = shakeRepository.ObterTodos();

            var usuarioLogado = ObterUsuarioSession();
            var nomeUsuarioLogado = ObterUsuarioNomeSession();
            if (!string.IsNullOrEmpty(nomeUsuarioLogado))
            {
            pedido.NomeUsuario = nomeUsuarioLogado;
            }

            var clienteLogado = clienteRepository.ObterPor(usuarioLogado);
            if(clienteLogado != null)
            {
                pedido.Cliente = clienteLogado;
            }
            pedido.NomeView = "Pedido";
            pedido.UsuarioEmail = ObterUsuarioSession();
            pedido.UsuarioNome = ObterUsuarioNomeSession();

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

            pedido.PrecoTotal = hamburguer.Preco + shake.Preco;

            if (pedidoRepository.Inserir (pedido)) {
                return View ("Sucesso", new RespostaViewModel()
                {
                    Mensagem = "Aguarde aprovação dos nossos administradores",
                    NomeView = "Sucesso",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            } else {
                return View ("Erro", new RespostaViewModel()
                {
                    Mensagem = "Houve um erro ao processsar seu pedido. Tente novamente!",
                    NomeView = "Erro",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }
    }
}