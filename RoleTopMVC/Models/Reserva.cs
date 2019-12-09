using System;
using RoleTopMVC.Enums;

namespace RoleTopMVC.Models
{
    public class Reserva
    {
        public ulong Id {get;set;}
        public Cliente Cliente {get;set;}
        public uint Status {get;set;}
        public string Nome_evento {get;set;}
        public DateTime Data_evento {get;set;}
        public int Quantidade {get;set;}
        public string Servicos {get;set;}
        public string Tipo_evento {get;set;}
        public string Pagamento {get;set;}

        public Reserva()
        {
            this.Cliente = new Cliente();
            this.Id = 0;
            this.Status = (uint) StatusReserva.PENDENTE;
        }
    }
}