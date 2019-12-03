using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class ClienteController : AbstractController
    {
        private ClienteRepository clienteRepository = new ClienteRepository();

        [HttpGet]
        public IActionResult Login()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Login",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            ViewData["Action"]="Login";
            try
            {
            System.Console.WriteLine("==========================");
            System.Console.WriteLine(form["email"]);
            System.Console.WriteLine(form["senha"]);
            System.Console.WriteLine("==========================");

            var usuario = form["email"];
            var senha = form["senha"];

            var cliente = clienteRepository.ObterPor(usuario);

            if (cliente != null)
            {
                if(cliente.Senha.Equals(senha))
                {   HttpContext.Session.SetString(SESSION_CLIENTE_EMAIL, usuario); //SetString guarda uma string e armazena  na session email
                    HttpContext.Session.SetString(SESSION_CLIENTE_NOME, cliente.Nome);
                    return RedirectToAction("Index", "Cliente");
                }
                else
                {
                    return View("Erro", new RespostaViewModel("Senha incorreta"));
                }
            }
            else
            {
                return View("Erro", new RespostaViewModel($"Usuário {usuario} não encontrado"));                
            }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return View("Erro");
            }
            
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Remove(SESSION_CLIENTE_EMAIL);
            HttpContext.Session.Remove(SESSION_CLIENTE_NOME);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }

    }
}