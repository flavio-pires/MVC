using System;
using System.Collections.Generic;
using System.IO;
using McBonaldsMVC.Models;

namespace McBonaldsMVC.Repositories
{
    public class PedidoRepository : RepositoryBase
    {
        private const string PATH = "Database/Pedido.csv";
        public PedidoRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Pedido pedido)
            {
                var quantidadeLinhas = File.ReadAllLines(PATH).Length;
                pedido.Id = (ulong) ++quantidadeLinhas;
                var linha = new string[] {PrepararRegistroCSV(pedido)};
                File.AppendAllLines(PATH, linha);

                return true;
            }

            public List<Pedido> ObterTodos()
            {
                var linhas = File.ReadAllLines(PATH);
                List<Pedido> pedidos = new List<Pedido>();
                foreach (var linha in linhas)
                {
                    Pedido pedido = new Pedido();
                    
                    pedido.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
                    pedido.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
                    pedido.Cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
                    pedido.Cliente.Endereco = ExtrairValorDoCampo("cliente_endereco", linha);
                    pedido.Cliente.Telefone = ExtrairValorDoCampo("cliente_telefone", linha);
                    pedido.Cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
                    pedido.Hamburguer.Nome = ExtrairValorDoCampo("hamburguer_nome", linha);
                    pedido.Hamburguer.Preco = double.Parse(ExtrairValorDoCampo("hamburguer_preco", linha));
                    pedido.Shake.Nome = ExtrairValorDoCampo("shake_nome", linha);
                    pedido.Shake.Preco = double.Parse(ExtrairValorDoCampo("shake_preco", linha));
                    pedido.DataDoPedido = DateTime.Parse(ExtrairValorDoCampo("data_pedido", linha));
                    pedido.PrecoTotal = double.Parse(ExtrairValorDoCampo("preco_total", linha));

                    pedidos.Add(pedido);
                }
                return pedidos;
            }

            public List<Pedido> ObterTodosPorCliente(string email)
            {
                var pedidosTotais = ObterTodos();
                List<Pedido> pedidosCliente = new List<Pedido>();
                foreach (var pedido in pedidosTotais)
                {
                    if(pedido.Cliente.Email.Equals(email))
                    {
                        pedidosCliente.Add(pedido);
                    }
                }
                return pedidosCliente;
            }

            public Pedido ObterPor(ulong id) // método para obter o Id dos pedidos
            {
                var pedidosTotais = ObterTodos();
                foreach (var pedido in pedidosTotais)
                {
                    if(pedido.Id == id) //condição para o banco verificar se o Id do pedido do cliente, encontra-se na tabela, para depois retornar o pedido com o status de aprovado, reprovado
                    {
                        return pedido;
                    }
                }
                return null;
            }

            public bool Atualizar(ulong id, Pedido pedido)
            {
                var pedidosTotais = File.ReadAllLines(PATH); // recolhe tudo que esta na tabela de pedidos
                var pedidoCSV = PrepararRegistroCSV(pedido); // transforma o pedido atualizado em string para ser gravado no CSV
                var linhaPedido = -1; // porque não haverá linha -1.. serve apenas para atualizar
                var resultado = false;

                for (int i=0; i < pedidosTotais.Length; i++)
                {
                    var idConvertido = ulong.Parse(ExtrairValorDoCampo("id", pedidosTotais[i]));
                    if(pedido.Id.Equals(idConvertido))  // se o ID do pedido que foi enviado para atualizar for igual a linha com o ID igual ele vai atualizar o status
                    {
                        linhaPedido = i;
                        resultado = true;
                        break;
                    }
                }

                if (resultado)
                {
                    pedidosTotais[linhaPedido] = pedidoCSV;
                    File.WriteAllLines(PATH, pedidosTotais);
                }

                return resultado;
            }

            private string PrepararRegistroCSV(Pedido pedido)
            {
                Cliente cliente = pedido.Cliente;
                Hamburguer hamburguer = pedido.Hamburguer;
                Shake shake = pedido.Shake;
                return $"id={pedido.Id};status_pedido={pedido.Status};cliente_nome={cliente.Nome};cliente_endereco={cliente.Endereco};cliente_telefone={cliente.Telefone};cliente_email={cliente.Email};hamburguer_nome={hamburguer.Nome};hamburguer_preco={hamburguer.Preco};shake_nome={shake.Nome};shake_preco={shake.Preco};data_pedido={pedido.DataDoPedido};preco_total={pedido.PrecoTotal}";
            }

    }
}