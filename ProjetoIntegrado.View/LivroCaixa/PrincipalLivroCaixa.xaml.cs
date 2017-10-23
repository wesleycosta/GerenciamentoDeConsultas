using System;
using System.Windows.Input;

namespace ProjetoIntegrado.View.LivroCaixa
{
    public partial class PrincipalLivroCaixa
    {
        public PrincipalLivroCaixa()
        {
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }
    }
}
