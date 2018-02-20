using System;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Faturamento
{
    public partial class PrincipalFaturamentoWin
    {
        public PrincipalFaturamentoWin()
        {
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }
    }
}
