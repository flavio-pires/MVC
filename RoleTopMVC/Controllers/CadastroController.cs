using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class CadastroController : AbstractController
    {
        ClienteRepository clienteRepositorio = new ClienteRepository();
        public IActionResult Index()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Cadastro",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }

        public IActionResult CadastrarCliente(IFormCollection form)
        {
            ViewData["Action"] = "Cadastro";
            try{
                Cliente cliente = new Cliente(form["email"], form["senha"], form["nome"], int.Parse(form["cpf"]), int.Parse(form["telefone"]), form["nome_evento"], DateTime.Parse(form["data_evento"]), int.Parse(form["quantidade"]), form["servicos"]);

                clienteRepositorio.Inserir(cliente);

            return View("Sucesso", new RespostaViewModel("Cadastro efetuado com sucesso"));
            } catch(Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
                return View("Erro", new RespostaViewModel("Erro no cadastro"));          
            }
            
        }
    }
}