using System;


namespace ProjetoIntegrado
{
    public static class Excecao
    {
        public static void Mostrar(Exception ex)
        {
            Mensagens.Mbox.Excecao(ex.ToString());
        }
    }
}
