using System;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class AdministradorController : AbstractController
    {
        ReservaRepository reservaRepository = new ReservaRepository();

        [HttpGet] // marcar que as requisições que chegarem a ele são do tipo Get.... se for do tipo POST é necessário mudar HttpPost
        public IActionResult Dashboard ()
        {
            try
            {
            var tipoUsuarioSessao = uint.Parse(ObterUsuarioTipoSession());
            if(tipoUsuarioSessao.Equals((uint)TiposUsuario.ADMINISTRADOR))
            {
            var reservas = reservaRepository.ObterTodos();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            foreach (var reserva in reservas)
            {
                switch(reserva.Status)
                {
                    case (uint) StatusReserva.REPROVADO:
                        dashboardViewModel.PedidosReprovados++;
                    break;
                    case (uint) StatusReserva.APROVADO:
                        dashboardViewModel.PedidosAprovados++;
                    break;
                    default:
                        dashboardViewModel.PedidosPendentes++;
                        dashboardViewModel.Reservas.Add(reserva);
                    break;
                }
            }
            dashboardViewModel.NomeView = "Dashboard";
            dashboardViewModel.UsuarioEmail = ObterUsuarioSession();

            return View(dashboardViewModel);
            }

            return View("Erro", new RespostaViewModel()
            {
                NomeView = "Dashboard",
                Mensagem = "Acesso restrito"
            });

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return View("Erro", new RespostaViewModel()
                {
                    NomeView = "Dashboard",
                    Mensagem = "Tempo esgotado. Faça login novamente!"
                });
            }
        }
    }
}