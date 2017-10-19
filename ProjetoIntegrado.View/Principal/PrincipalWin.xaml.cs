using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Principal
{
    using Model;
    using View;
    using ViewUtil;
    using Mensagens;

    public partial class JanelaPrincipalWin
    {
        private readonly Window frmLogin;

        public JanelaPrincipalWin(Window frmLogin)
        {
            InitializeComponent();
            IniciarIcones();

            this.frmLogin = frmLogin;
            tbUsuario.Text = Sessao.funcionario.usuario;

            Closing += (o, a) =>
                a.Cancel = !Sair();
        }

        private void IniciarIcones()
        {
            // LOGO
            imgLogo.BitmapToImageSource(Icons.eyeglasses);

            // CONSULTAS
            imgAgenda.BitmapToImageSource(Icons.Time_16x16);
            imgPacientes.BitmapToImageSource(Icons.Customer_16x16);

            // CADASTROS
            imgEmpresa.BitmapToImageSource(Icons.Home_16x16);
            imgFuncionarios.BitmapToImageSource(Icons.Employee_16x16);
            imgUsuarios.BitmapToImageSource(Icons.Team_16x16);
            imgCargos.BitmapToImageSource(Icons.PackageProduct_16x16);
            imgCategoria.BitmapToImageSource(Icons.Palette_16x16);

            // FINANCEIRO
            imgFormaDePagamento.BitmapToImageSource(Icons.FullStackedBar_16x16);
            imgFluxoDeCaixa.BitmapToImageSource(Icons.FluxoDeCaixa16x16);
            imgLivroCaixa.BitmapToImageSource(Icons.Content_16x16);
            imgFaturamento.BitmapToImageSource(Icons.Chart_32x32);

            // RELATORIOS E CONFIGURACOES
            imgRelatorios.BitmapToImageSource(Icons.Report_16x16);
            imgConfiguracoes.BitmapToImageSource(Icons.Properties_16x16);
        }

        private bool Sair()
        {
            var r = Mbox.DesejaSair();
            return r == MessageDialogResult.Affirmative;
        }

        #region EVENTOS

        // ON LOADED
        private void JanelaPrincipal_OnLoaded(object sender, RoutedEventArgs e)
        {
            Mbox.JanelaPrincipal = this;
            frmLogin.Close();
        }

        // DOUBLE CLICK ITEM MENU
        private void Menu_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MenuItens.MantemItem((sender as TreeViewItem)?.Uid);
        }

        // KEY DOWN
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        #endregion
    }
}
