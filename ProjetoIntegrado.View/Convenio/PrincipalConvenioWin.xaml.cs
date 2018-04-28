using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using ProjetoIntegrado.Funcoes;

namespace ProjetoIntegrado.View.Convenio
{
    using Model;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalConvenioWin
    {
        private List<ConvenioModel> listaDeConvenios;

        public PrincipalConvenioWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarConvenios();
        }

        #region MANTEM CARGO

        private void CarregarConvenios()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                listaDeConvenios = ConvenioModel.CarregarTodos();
            else
                listaDeConvenios = ConvenioModel.Pesquisar(tbPesquisa.Text);

            lvwCargos.ItemsSource = listaDeConvenios;
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadConvenio = new CadConvenioWin();
            cadConvenio.ShowDialog();

            if (cadConvenio.cadastrou)
                CarregarConvenios();
        }

        private void Editar()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var convenio = lvwCargos.SelectedItems[0] as ConvenioModel;
                var cadConvenio = new CadConvenioWin(convenio);
                cadConvenio.ShowDialog();

                if (cadConvenio.cadastrou)
                    CarregarConvenios();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var convenio = lvwCargos.SelectedItems[0] as ConvenioModel;
                    convenio?.Remover();

                    listaDeConvenios.Remove(convenio);
                    lvwCargos.Items.Refresh();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void BtnNovo_OnClick(object sender, RoutedEventArgs e) => 
            Novo();

        private void lvwCargos_MouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            Editar();

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarConvenios();

            if (e.Key == Key.Down)
                lvwCargos.SelecionarPrimeiraLinha();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void lvwCargos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
            else if (e.Key == Key.Delete)
                Remover();
        }

        #endregion
    }
}
