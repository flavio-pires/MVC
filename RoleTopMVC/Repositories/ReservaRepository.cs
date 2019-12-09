using System;
using System.Collections.Generic;
using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories
{
    public class ReservaRepository : RepositoryBase
    {
        private const string PATH = "Database/Reserva.csv";

        public ReservaRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Reserva reserva)
            {
                var quantidadeLinhas = File.ReadAllLines(PATH).Length;
                reserva.Id = (ulong) ++quantidadeLinhas;
                var linha = new string[] {PrepararRegistroCSV(reserva)};
                File.AppendAllLines(PATH, linha);

                return true;
            }

            public List<Reserva> ObterTodos()
            {
                var linhas = File.ReadAllLines(PATH);
                List<Reserva> reservas = new List<Reserva>();
                foreach (var linha in linhas)
                {
                    Reserva reserva = new Reserva();
                    
                    reserva.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
                    reserva.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
                    reserva.Cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
                    reserva.Cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
                    reserva.Nome_evento = ExtrairValorDoCampo("nome_evento", linha);
                    reserva.Data_evento = DateTime.Parse(ExtrairValorDoCampo("data_evento", linha));
                    reserva.Quantidade = int.Parse(ExtrairValorDoCampo("quantidade", linha));
                    reserva.Servicos = ExtrairValorDoCampo("servicos", linha);
                    reserva.Tipo_evento = ExtrairValorDoCampo("tipo_evento", linha);
                    reserva.Pagamento = ExtrairValorDoCampo("pagamento", linha);

                    reservas.Add(reserva);
                }
                return reservas;
            }

            public List<Reserva> ObterTodosPorCliente(string email)
            {
                var reservasTotais = ObterTodos();
                List<Reserva> reservasCliente = new List<Reserva>();
                foreach (var reserva in reservasTotais)
                {
                    if(reserva.Cliente.Email.Equals(email))
                    {
                        reservasCliente.Add(reserva);
                    }
                }
                return reservasCliente;
            }

            public Reserva ObterPor(ulong id) // método para obter o Id dos pedidos
            {
                var reservasTotais = ObterTodos();
                foreach (var reserva in reservasTotais)
                {
                    if(reserva.Id == id) //condição para o banco verificar se o Id do pedido do cliente, encontra-se na tabela, para depois retornar o pedido com o status de aprovado, reprovado
                    {
                        return reserva;
                    }
                }
                return null;
            }

            public bool Atualizar(ulong id, Reserva reserva)
            {
                var reservasTotais = File.ReadAllLines(PATH); // recolhe tudo que esta na tabela de pedidos
                var reservaCSV = PrepararRegistroCSV(reserva); // transforma o pedido atualizado em string para ser gravado no CSV
                var linhaReserva = -1; // porque não haverá linha -1.. serve apenas para atualizar
                var resultado = false;

                for (int i=0; i < reservasTotais.Length; i++)
                {
                    var idConvertido = ulong.Parse(ExtrairValorDoCampo("id", reservasTotais[i]));
                    if(reserva.Id.Equals(idConvertido))  // se o ID do pedido que foi enviado para atualizar for igual a linha com o ID igual ele vai atualizar o status
                    {
                        linhaReserva = i;
                        resultado = true;
                        break;
                    }
                }

                if (resultado)
                {
                    reservasTotais[linhaReserva] = reservaCSV;
                    File.WriteAllLines(PATH, reservasTotais);
                }

                return resultado;
            }

            private string PrepararRegistroCSV(Reserva reserva)
            {
                Cliente cliente = reserva.Cliente;
                return $"id={reserva.Id};status_pedido={reserva.Status};cliente_nome={cliente.Nome};cliente_email={cliente.Email};nome_evento={reserva.Nome_evento};quantidade={reserva.Quantidade};servicos={reserva.Servicos};tipo_evento={reserva.Tipo_evento};pagamento={reserva.Pagamento};data_evento={reserva.Data_evento}";
            }

    }
}