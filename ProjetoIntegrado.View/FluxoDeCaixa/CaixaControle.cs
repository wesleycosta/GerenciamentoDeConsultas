namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    using Model;

    public static class CaixaControle
    {
        public static bool MantemCaixa()
        {
            var caixa = new CaixaModel();
            caixa.Carregar();
            var caixaAberto = false;

            if (!caixa?.caixaAberto ?? false)
                caixaAberto = AbrirCaixa();
            else
            {
                Sessao.caixa = caixa;
                caixaAberto = true;
            }

            return caixaAberto;
        }

        public static void CarregarSessao()
        {
            var caixa = new CaixaModel();
            caixa.Carregar();
            Sessao.caixa = caixa;
        }

        private static bool AbrirCaixa()
        {
            var frmAbrirCaixa = new AbrirCaixaWin();
            frmAbrirCaixa.ShowDialog();

            return frmAbrirCaixa.abriuCaixa;
        }
    }
}
