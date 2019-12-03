namespace RoleTopMVC.Models
{
    public class Reserva
    {
        public ulong Id {get;set;}
        public Cliente Cliente {get;set;}

        public Reserva()
        {
            this.Cliente = new Cliente();
            this.Id = 0;
            this.Status = (uint) StatusPedido.PENDENTE;
        }
    }
}