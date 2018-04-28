namespace ProjetoIntegrado.Model
{
    public static class MetodoDeExtensao
    {
        public static string ToStringFormatado(this decimal valor, int tamanho = 10)
        {
            var str = valor.ToString("n");

            if (!str.Contains(".")) tamanho++;
            if (!str.Contains(",")) tamanho++;

            return str.PadLeft(tamanho, ' ');
        }
    }
}
