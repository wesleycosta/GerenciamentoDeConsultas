using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjetoIntegrado.ViewUtil;

namespace ProjetoIntegrado.View.Relatorios
{
    using Mensagens;
    using Model;
    using Principal;

    public partial class RelatorioWins
    {
        #region  PROPRIEDADES E CTOR

        private object item;

        public RelatorioWins()
        {
            InitializeComponent();
            IniciarIcones();
            IniciarPermissoes();
        }

        private void IniciarPermissoes()
        {
            var nivel = new NivelAcesso(Sessao.funcionario.cargo);

            itemProcedimentos.IsEnabled = nivel.AcessoProcedimentos;

            itemDespesas.IsEnabled = nivel.AcessoProcedimentos;
            itemEntradas.IsEnabled = nivel.AcessoProcedimentos;
            itemFaturamento.IsEnabled = nivel.AcessoProcedimentos;
        }


        private void IniciarIcones()
        {
            imgListaConsultas.BitmapToImageSource(Icons.WorkWeekView_16x16);
            imgCancelada.BitmapToImageSource(Icons.InsertHeader_16x16);
            imgProcedimentos.BitmapToImageSource(Icons.Time_16x16);

            imgEntradas.BitmapToImageSource(Icons.AlignHorizontalBottom2_16x16);
            imgDespesas.BitmapToImageSource(Icons.SwitchTimeScalesTo_16x16);
            imgFaturamento.BitmapToImageSource(Icons.Chart_16x16);
        }

        #endregion

        #region  EVENTOS

        private void btnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (item != null)
                RelatorioItens.MantemItem((item as TreeViewItem)?.Uid);
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) => Close();

        private void Menu_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            RelatorioItens.MantemItem((sender as TreeViewItem)?.Uid);

        private void TreeViewItem_OnItemSelected(object sender, RoutedEventArgs e) => item = sender;

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
