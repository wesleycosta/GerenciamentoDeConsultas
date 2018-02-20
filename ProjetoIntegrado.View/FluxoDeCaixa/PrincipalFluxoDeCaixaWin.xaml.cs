using System.Windows.Input;

namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    public partial class PrincipalFluxoDeCaixaWin
    {
        public PrincipalFluxoDeCaixaWin()
        {
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }

        private void BtnAbrir_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var frmAbrir = new AbrirCaixaWin();
            frmAbrir.ShowDialog();
        }

        private void BtnFechar_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
