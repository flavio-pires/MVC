
using System;

namespace McBonaldsMVC.Models
{
    public class Cliente // modelos servem para representar um objeto do mundo real dentro do sistema... modelo descreve o que é um cliente para o sistema, descrevendo as caracteristicas e comportamentos
    {
        public string Nome {get;set;}
        public string Endereco {get;set;}
        public string Telefone {get;set;}
        public string Senha {get;set;}
        public string Email {get;set;}
        public DateTime DataNascimento {get;set;}
        public uint TipoUsuario {get;set;}

        public Cliente()
        {
            
        }
        public Cliente(string nome, string endereco, string telefone, string senha, string email, DateTime dataNascimento)
        {
            this.Nome = nome;
            this.Endereco = endereco;
            this.Telefone = telefone;
            this.Senha = senha;
            this.Email = email;
            this.DataNascimento = dataNascimento;
        }
    }
    
}