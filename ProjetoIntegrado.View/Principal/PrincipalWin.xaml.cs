using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using ProjetoIntegrado.Funcoes;

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
            this.frmLogin = frmLogin;
            MenuItens.Evento = EventoItem;

            IniciarIcones();
            CarregarUsuario();

            Closing += (o, a) =>
                a.Cancel = !Sair();

            ValidarCampos.JanelaPrincipal = this;
        }

        private void CarregarUsuario() => tbUsuario.Text = Sessao.funcionario.usuario;

        private void IniciarIcones()
        {
            // LOGO
            imgLogo.BitmapToImageSource(Icons.eyeglasses);

            // PRINCIPAL
            imgPacientes.BitmapToImageSource(Icons.Customer_16x16);
            imgProcedimentos.BitmapToImageSource(Icons.Time_16x16);
            
            // CADASTROS
            imgEmpresa.BitmapToImageSource(Icons.Home_16x16);
            imgFuncionarios.BitmapToImageSource(Icons.Employee_16x16);
            imgUsuarios.BitmapToImageSource(Icons.Team_16x16);
            imgCargos.BitmapToImageSource(Icons.PackageProduct_16x16);
            imgConvenio.BitmapToImageSource(Icons.Contact_16x16);

            // FINANCEIRO
            imgFormaDePagamento.BitmapToImageSource(Icons.FullStackedBar_16x16);
            imgFluxoDeCaixa.BitmapToImageSource(Icons.FluxoDeCaixa16x16);
            imgDespesas.BitmapToImageSource(Icons.SwitchTimeScalesTo_16x16);

            // RELATORIOS E CONFIGURACOES
            imgRelatorios.BitmapToImageSource(Icons.Report_16x16);
            imgTrocarUsuario.BitmapToImageSource(Icons.Project_16x16);
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
            Mbox.JanelaPrincipal =  this;
            frmLogin.Close();
        }

        // DOUBLE CLICK ITEM MENU
        private void Menu_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) => MenuItens.MantemItem((sender as TreeViewItem)?.Uid);

        private void EventoItem(object sender, EventArgs e)
        {
            var item = MenuItens.GetItem(sender.ToString());

            if (item == MenuItensEnum.TrocarUsuario)
                CarregarUsuario();
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
