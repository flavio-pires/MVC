using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class ReservaViewModel : BaseViewModel
    {
        public Cliente Cliente {get;set;}

        public ReservaViewModel()
        {
            this.Cliente = new Cliente();
        }
    }
}