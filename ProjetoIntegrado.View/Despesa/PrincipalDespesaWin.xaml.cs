using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Despesa
{
    using Model;
    using Funcoes;
    using Mensagens;
    using ViewUtil;
    using System.Linq;

    public partial class PrincipalDespesaWin
    {
        private List<DespesaModel> lDespesas;
        private bool iniciou;

        public PrincipalDespesaWin()
        {
            InitializeComponent();

            tbDataInicial.SelectedDate = DataUtil.GetPrimeiroDiaDesseMes();
            tbDataFinal.SelectedDate = DataUtil.GetUltimoDiaDesseMes();

            Loaded += (o, a) =>
            {
                CarregarDespesas();
                iniciou = true;
            };
        }

        #region MANTEM DESPESAS

        private void CarregarDespesas()
        {
            if (ValidarCampos.Validar(this))
            {
                lDespesas = DespesaModel.Pesquisar(tbPesquisa.Text, tbDataInicial.SelectedDate.Value, tbDataFinal.SelectedDate.Value);
                lvwFuncionarios.ItemsSource = lDespesas;

                lbTotalRegistro.Content = lDespesas.Count.ToString("D3");
                lbTotal.Content = lDespesas.Sum(x => x.valor).ToString("n");

                tbPesquisa.Focus();
            }
        }

        private void Novo()
        {
            var cadDespesas = new CadDespesaWin();
            cadDespesas.ShowDialog();

            if (cadDespesas.cadastrou)
                CarregarDespesas();
        }

        private void Editar()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var despesa = lvwFuncionarios.SelectedItems[0] as DespesaModel;
                var cadCliente = new CadDespesaWin(despesa);

                cadCliente.ShowDialog();

                if (cadCliente.cadastrou)
                    CarregarDespesas();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var despesa = lvwFuncionarios.SelectedItems[0] as DespesaModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    despesa?.Remover();
                    CarregarDespesas();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void BtnNovo_OnClick(object sender, RoutedEventArgs e) => Novo();

        private void lvwFuncionarios_MouseDoubleClick(object sender, MouseButtonEventArgs e) => Editar();

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarDespesas();

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

        private void tbDataInicial_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (iniciou)
                CarregarDespesas();
        }

        #endregion
    }
}
