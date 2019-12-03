using System;
using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories
{
    public class ClienteRepository
    {
        private const string PATH = "Database/Cliente.csv";
        public ClienteRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Cliente cliente)
        {
            var linha = new string[] {PrepararRegistroCSV(cliente)};
            File.AppendAllLines(PATH, linha);

            return true;
        }

        public Cliente ObterPor(string email)
        {
            var linhas = File.ReadAllLines(PATH);
            foreach(var linha in linhas)
            {
                if(ExtrairValorDoCampo("email",linha).Equals(email))
                {
                    Cliente c = new Cliente();
                    c.Email = ExtrairValorDoCampo("email", linha);
                    c.Senha = ExtrairValorDoCampo("senha", linha);
                    c.Nome = ExtrairValorDoCampo("nome", linha);
                    c.CPF = int.Parse(ExtrairValorDoCampo("cpf", linha));
                    c.Telefone = int.Parse(ExtrairValorDoCampo("telefone", linha));
                    c.Nome_evento = ExtrairValorDoCampo("nome_evento", linha);
                    c.Data_evento = DateTime.Parse(ExtrairValorDoCampo("data_evento", linha));
                    c.Quantidade = int.Parse(ExtrairValorDoCampo("quantidade", linha));
                    c.Servicos = ExtrairValorDoCampo("servicos", linha);

                    return c;
                }
            }

            return null;
        }

        private string PrepararRegistroCSV(Cliente cliente)
        {
            return $"email={cliente.Email};senha={cliente.Senha};nome={cliente.Nome};cpf={cliente.CPF};telefone={cliente.Telefone};nome_evento={cliente.Nome_evento};data_evento={cliente.Data_evento};quantidade={cliente.Quantidade};servicos={cliente.Servicos}";
        }

        public string ExtrairValorDoCampo(string nomeCampo, string linha)
            {
                var chave = nomeCampo;
                var indiceChave = linha.IndexOf(chave); //Indexof encontra a posição da chave que foi indicada, no caso "email"
                var indiceTerminal = linha.IndexOf(";",indiceChave);
                var valor = "";

                if(indiceTerminal != -1)
                {
                    valor = linha.Substring(indiceChave, indiceTerminal - indiceChave); //ignora a chave e pega o valor de string depois dela
                }
                else
                {
                    valor = linha.Substring(indiceChave);
                }

                System.Console.WriteLine($"Campo {nomeCampo} tem valor {valor}");
                return valor.Replace(nomeCampo + "=", "");
            }


    }
}