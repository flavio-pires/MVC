namespace McBonaldsMVC.Models
{
    public class Shake : Produto
    {
        public Shake()
        {

        }

        public Shake(string nome, double preco)
        {
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}