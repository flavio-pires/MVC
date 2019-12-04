using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class ReservaViewModel : BaseViewModel
    {
        public Cliente Cliente {get;set;}
        public string NomeUsuario {get;set;}

        public ReservaViewModel()
        {
            this.Cliente = new Cliente();
            this.NomeUsuario = "Amigão";
        }
    }
}