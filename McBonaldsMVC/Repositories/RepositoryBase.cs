namespace McBonaldsMVC.Repositories
{
    public class RepositoryBase
    {
        public string ExtrairValorDoCampo(string nomeCampo, string linha)
            {
                var chave = nomeCampo;
                var indiceChave = linha.IndexOf(chave); //Indexof encontra a posição da chave que foi indicada, no caso "email"
                var indiceTerminal = linha.IndexOf(";",indiceChave);
                var valor = "";

                if(indiceTerminal != -1)
                {
                    valor = linha.Substring(indiceChave, indiceTerminal - indiceChave); //ignora a chave e pega o valor de string depois dela
                }
                else
                {
                    valor = linha.Substring(indiceChave);
                }

                System.Console.WriteLine($"Campo {nomeCampo} tem valor {valor}");
                return valor.Replace(nomeCampo + "=", ""); // apaga o "email=" e substitui por nada
            }
    }
}