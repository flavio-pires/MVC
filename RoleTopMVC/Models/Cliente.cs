using System;

namespace RoleTopMVC.Models
{
    public class Cliente
    {
        public string Email {get;set;}
        public string Senha {get;set;}
        public string Nome {get;set;}
        public ulong CPF {get;set;}
        public ulong Telefone {get;set;}
        public uint TipoUsuario {get;set;}


        public Cliente()
        {

        }

        public Cliente (string email, string senha, string nome, ulong cpf, ulong telefone)
        {
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
            this.CPF = cpf;
            this.Telefone = telefone;

        }
    }
}