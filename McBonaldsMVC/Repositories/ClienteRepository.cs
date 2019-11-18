using System;
using System.IO;
using McBonaldsMVC.Models;

namespace McBonaldsMVC.Repositories
{
    public class ClienteRepository
    {
        private const string PATH = "Database/Cliente.csv"; // constante com o caminho do arquivo onde o File irá buscar... para representar que é uma constante, deixar em MAIUSCULO
        public ClienteRepository() // construtor ira verificar se existe o arquivo no database, caso não ele irá criar
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close(); //Cria o arquivo e depois fecha, para não ter edições
            }

        }
            public bool Inserir(Cliente cliente) // boleano para retornar se deu certo ou não.. o método inserir irá receber o objeto Cliente da View, com os dados inseridos pelo usuário
            {
                var linha = new string[] {PrepararRegistroCSV(cliente)};
                File.AppendAllLines(PATH, linha); //método Append irá inserir os dados abaixo do outro.. PATH é o caminho onde será gravado o CSV

                return true;
            }

            public Cliente ObterPor(string email)
            {
                var linhas = File.ReadAllLines(PATH);
                foreach(var linha in linhas)
                {
                    //Cliente c = new Cliente();
                    //var dados = cliente.Split(";");
                    //c.Nome= dados[0];
                    //c.Email = dados[1];
                    //c.Senha = dados[2];

                    if(ExtrairValorDoCampo("email", linha).Equals(email)) // extrair o valor do campo email de cada linha... se o que extraiu for igual o que esta no campo email executa
                    {
                        Cliente c = new Cliente();
                        c.Nome = ExtrairValorDoCampo("nome", linha);
                        c.Email = ExtrairValorDoCampo("email", linha);
                        c.Senha = ExtrairValorDoCampo("senha", linha);
                        c.Endereco = ExtrairValorDoCampo("endereco", linha);
                        c.Telefone = ExtrairValorDoCampo("telefone", linha);
                        c.DataNascimento = DateTime.Parse(ExtrairValorDoCampo("dataNascimento", linha));

                        return c;
                    }                    
                }
                return null;
            }

            private string PrepararRegistroCSV(Cliente cliente)
            {
                return $"nome={cliente.Nome};email={cliente.Email};senha={cliente.Senha};endereco={cliente.Endereco};telefone={cliente.Telefone};data_nascimento={cliente.DataNascimento}";
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
                return valor.Replace(nomeCampo + "=", ""); // apaga o "email=" e substitui por nada
            }
    }
}