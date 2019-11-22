using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class AbstractController : Controller
    {
        protected const string SESSION_CLIENTE_EMAIL = "SESSION_CLIENTE_EMAIL";
        protected const string SESSION_CLIENTE_NOME = "SESSION_CLIENTE_NOME";
        protected string ObterUsuarioSession()
        {
            var email = HttpContext.Session.GetString(SESSION_CLIENTE_EMAIL);
            if (!string.IsNullOrEmpty(email)) // se o campo do email for diferente de nulo, retorna o email
            {
                return email;
            }
            else
            {
                return "";
            }
        }

        protected string ObterUsuarioNomeSession()
        {
            var nome = HttpContext.Session.GetString(SESSION_CLIENTE_NOME);
            if (!string.IsNullOrEmpty(nome)) // se o campo do nome for diferente de nulo, retorna o nome do cliente
            {
                return nome;
            }
            else
            {
                return "";
            }
        }
    }
}