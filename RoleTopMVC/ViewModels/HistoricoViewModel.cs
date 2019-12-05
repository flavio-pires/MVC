using System.Collections.Generic;
using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class HistoricoViewModel : BaseViewModel
    {
        public List<Reserva> Reservas {get;set;}
    }
}