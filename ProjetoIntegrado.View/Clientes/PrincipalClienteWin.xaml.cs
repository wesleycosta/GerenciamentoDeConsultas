using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Clientes
{
    using Model;
    using Funcoes;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalClienteWin
    {
        private List<ClienteModel> lClientes;

        public PrincipalClienteWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarFuncionarios();
        }

        #region MANTEM CLIENTES

        private FiltroPessoa GetFiltro() =>
           (FiltroPessoa)Enum.Parse(typeof(FiltroPessoa), Mascara.Remover(cbFiltro.Text.ToLower()));

        private void CarregarFuncionarios()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lClientes = ClienteModel.CarregarTodos();
            else
                lClientes = ClienteModel.Pesquisar(GetFiltro(), tbPesquisa.Text);

            lvwFuncionarios.ItemsSource = lClientes;
            lbTotalRegistro.Content = lClientes.Count.ToString("D3");
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadCliente = new CadClienteWin();
            cadCliente.ShowDialog();

            if (cadCliente.cadastrou)
                CarregarFuncionarios();
        }

        private void Editar()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var cliente = lvwFuncionarios.SelectedItems[0] as ClienteModel;
                var cadCliente = new CadClienteWin(cliente);

                cadCliente.ShowDialog();

                if (cadCliente.cadastrou)
                    CarregarFuncionarios();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var cliente = lvwFuncionarios.SelectedItems[0] as ClienteModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    cliente?.Remover();
                    lClientes.Remove(cliente);
                    lvwFuncionarios.Items.Refresh();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void BtnNovo_OnClick(object sender, RoutedEventArgs e)
        {
            Novo();
        }

        private void lvwFuncionarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarFuncionarios();

            if (e.Key == Key.Down)
                lvwFuncionarios.SelecionarPrimeiraLinha();
        }

        private void lvwFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
            else if (e.Key == Key.Delete)
                Remover();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
