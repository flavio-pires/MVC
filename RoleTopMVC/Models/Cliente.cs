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
        public string Nome_evento {get;set;}
        public DateTime Data_evento {get;set;}
        public int Quantidade {get;set;}


        public Cliente()
        {

        }

        public Cliente (string email, string senha, string nome, int cpf, int telefone, string nome_evento, DateTime data_evento, int quantidade)
        {
            this.Email = email;
            this.Senha = senha;
            this.Nome = nome;
            this.CPF = cpf;
            this.Telefone = telefone;
            this.Nome_evento = nome_evento;
            this.Data_evento = data_evento;
            this.Quantidade = quantidade;
        }
    }
}