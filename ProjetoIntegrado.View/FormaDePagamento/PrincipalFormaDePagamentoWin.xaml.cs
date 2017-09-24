using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.FormaDePagamento
{
    using Model;

    public partial class PrincipalFormaDePagamentoWin 
    {
        List<FormaDePagamentoModel> lFormaDePagamento = new List<FormaDePagamentoModel>();

        public PrincipalFormaDePagamentoWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarFormaDePagamento();
        }

        #region MANTEM CARGO

        private void CarregarFormaDePagamento()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lFormaDePagamento = FormaDePagamentoModel.CarregarTodos();
            else
                lFormaDePagamento = FormaDePagamentoModel.Pesquisar(tbPesquisa.Text);

            lvwCategorias.ItemsSource = lFormaDePagamento;
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadFormaPagamento = new CadFormaDePagamentoWin();
            cadFormaPagamento.ShowDialog();

            if (cadFormaPagamento.cadastrou)
                CarregarFormaDePagamento();
        }

        private void Editar()
        {
            if (lvwCategorias.SelectedIndex >= 0)
            {
                var formaDePagamento = lvwCategorias.SelectedItems[0] as FormaDePagamentoModel;
                var cadFormaDePagamento = new CadFormaDePagamentoWin(formaDePagamento);
                cadFormaDePagamento.ShowDialog();

                if (cadFormaDePagamento.cadastrou)
                    CarregarFormaDePagamento();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwCategorias.SelectedIndex >= 0)
            {
                var formaDePagamento = lvwCategorias.SelectedItems[0] as FormaDePagamentoModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    formaDePagamento.Remover();
                    lFormaDePagamento.Remove(formaDePagamento);
                    lvwCategorias.Items.Refresh();
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

        private void lvwCargos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarFormaDePagamento();

            if (e.Key == Key.Down)
                lvwCategorias.SelecionarPrimeiraLinha();
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
