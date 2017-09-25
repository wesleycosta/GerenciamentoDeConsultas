using System.Linq;
using System.Collections.Generic;

namespace ProjetoIntegrado.Funcoes
{
    public static class Mascara
    {
        private static readonly List<string> caracteres = new List<string>()
        {
            " ",
            ".",
            ",",
            "-",
            "/",
            "\\"
        };

        public static string Remover(string texto)
        {
            caracteres.ForEach(x => texto = texto.Replace(x, ""));

            return texto;
        }
    }
}
