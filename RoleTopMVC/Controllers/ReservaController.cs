using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            };

            reserva.Cliente = cliente;

            reserva.Data_evento = DateTime.Now;

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
    }
}