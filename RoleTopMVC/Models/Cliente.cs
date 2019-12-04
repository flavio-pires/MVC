using System;

namespace RoleTopMVC.Models
{
    public class Cliente
    {
        public string Email {get;set;}
        public string Senha {get;set;}
        public string Nome {get;set;}
        public int CPF {get;set;}
        public int Telefone {get;set;}
        public uint TipoUsuario {get;set;}


        public Cliente()
        {

        }

        public Cliente (string email, string senha, string nome, int cpf, int telefone)
        {
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
            this.CPF = cpf;
            this.Telefone = telefone;

        }
    }
}