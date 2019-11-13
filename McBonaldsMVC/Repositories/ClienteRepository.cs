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

            private string PrepararRegistroCSV(Cliente cliente)
            {
                return $"nome={cliente.Nome};email={cliente.Email};senha={cliente.Senha};endereco={cliente.Endereco};telefone={cliente.Telefone};data_nascimento={cliente.DataNascimento}";
            }
    }
}