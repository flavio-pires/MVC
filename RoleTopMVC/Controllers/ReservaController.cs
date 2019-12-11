using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class ReservaController : AbstractController
    {
        ReservaRepository reservaRepository = new ReservaRepository();
        ClienteRepository clienteRepository = new ClienteRepository();

        public IActionResult Index()
        {
            ReservaViewModel reserva = new ReservaViewModel();

            var usuarioLogado = ObterUsuarioSession();

            var clienteLogado = clienteRepository.ObterPor(usuarioLogado);
            if(clienteLogado != null)
            {
                reserva.Cliente = clienteLogado;
            }
            reserva.NomeView = "Reserva";
            reserva.UsuarioEmail = ObterUsuarioSession();
            reserva.UsuarioNome = ObterUsuarioNomeSession();

            return View (reserva);
        }
        
        public IActionResult Solicitar (IFormCollection form) {
            ViewData["Action"] = "Reserva";
            Reserva reserva = new Reserva ();

            Cliente cliente = new Cliente () {
                Nome = form["nome"],
                Email = form["email"]
            };

            reserva.Cliente = cliente;
            reserva.Nome_evento = form["nome_evento"];
            reserva.Quantidade = int.Parse(form["quantidade"]);
            reserva.Servicos = form["servicos"];
            reserva.Tipo_evento = form["tipo_evento"];
            reserva.Pagamento = form["pagamento"];
            reserva.Data_evento = DateTime.Parse(form["data_evento"]);

            if (reservaRepository.Inserir (reserva)) {
                return View ("Sucesso", new RespostaViewModel()
                {
                    Mensagem = "Aguarde aprovação dos nossos administradores!",
                    NomeView = "Sucesso",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            } else {
                return View ("Erro", new RespostaViewModel()
                {
                    Mensagem = "Houve um erro ao processsar seu agendamento. Tente novamente!",
                    NomeView = "Erro",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }

        public IActionResult Aprovar(ulong id) 
        {
            Reserva reserva = reservaRepository.ObterPor(id);
            reserva.Status = (uint) StatusReserva.APROVADO;

            if(reservaRepository.Atualizar(id,reserva))
            {
                return RedirectToAction("Dashboard", "Administrador");
            }
            else
            {
                return View ("Erro", new RespostaViewModel()
                {
                    Mensagem = "Houve um erro ao aprovar seu pedido.",
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }

        public IActionResult Reprovar(ulong id) 
        {
            Reserva reserva = reservaRepository.ObterPor(id);
            reserva.Status = (uint) StatusReserva.REPROVADO;

            if(reservaRepository.Atualizar(id,reserva))
            {
                return RedirectToAction("Dashboard", "Administrador");
            }
            else
            {
                return View ("Erro", new RespostaViewModel()
                {
                    Mensagem = "Houve um erro ao reprovar seu pedido.",
                    NomeView = "Dashboard",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }
    }
}