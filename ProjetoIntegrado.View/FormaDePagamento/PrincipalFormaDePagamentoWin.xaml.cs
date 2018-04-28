using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.FormaDePagamento
{
    using Model;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalFormaDePagamentoWin
    {
        private List<FormaDePagamentoModel> lFormaDePagamento;

        public PrincipalFormaDePagamentoWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarFormaDePagamento();
        }

        #region MANTEM FORMA DE PAGAMENTO

        private void CarregarFormaDePagamento()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lFormaDePagamento = FormaDePagamentoModel.CarregarTodos();
            else
                lFormaDePagamento = FormaDePagamentoModel.Pesquisar(tbPesquisa.Text);

            lvwFormaDePagamento.ItemsSource = lFormaDePagamento;
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
            if (lvwFormaDePagamento.SelectedIndex >= 0)
            {
                var formaDePagamento = lvwFormaDePagamento.SelectedItems[0] as FormaDePagamentoModel;
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
            if (lvwFormaDePagamento.SelectedIndex >= 0)
            {
                var formaDePagamento = lvwFormaDePagamento.SelectedItems[0] as FormaDePagamentoModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    formaDePagamento?.Remover();
                    lFormaDePagamento.Remove(formaDePagamento);
                    lvwFormaDePagamento.Items.Refresh();
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

        private void lvwFormaDePagamento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarFormaDePagamento();

            if (e.Key == Key.Down)
                lvwFormaDePagamento.SelecionarPrimeiraLinha();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void lvwFormaDePagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
            else if (e.Key == Key.Delete)
                Remover();
        }

        #endregion
    }
}
