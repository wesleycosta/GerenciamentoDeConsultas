using System.Linq;
using System.Collections.Generic;

namespace ProjetoIntegrado.Funcoes
{
    public static class Mascara
    {
        #region LISTA

        private static readonly List<string> caracteres = new List<string>
        {
            " ",
            ".",
            ",",
            "-",
            "/",
            "\\",
            "(",
            ")"
        };

        #endregion

        public static string Remover(string texto)
        {
            caracteres.ForEach(x => texto = texto.Replace(x, ""));

            return texto;
        }
    }
}
