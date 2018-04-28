using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Consultas
{
    using Model;
    using ViewUtil;
    using Mensagens;

    public partial class ExcluirConsultaWin
    {
        private object item;

        public bool Removeu { get; set; }
        private ConsultaModel consulta;

        public ExcluirConsultaWin(ConsultaModel consulta)
        {
            InitializeComponent();
            this.consulta = consulta;

            imgNaoCompareceu.BitmapToImageSource(Icons.GroupFooter_16x16);
            imgConsultaCancelada.BitmapToImageSource(Icons.InsertHeader_16x16);
            imgConsultaRemarcada.BitmapToImageSource(Icons.InsertFooter_16x16);
        }

        private void MantemItem(string itemSelecionado)
        {
            var tipo = (TipoDeCancelamento)(Enum.Parse(typeof(TipoDeCancelamento), itemSelecionado));

            if (tipo == TipoDeCancelamento.Remarcado)
                Remarcar();
            else
            {
                consulta.Cancelar(tipo);
                Removeu = true;
                Close();
            }
        }

        private void Remarcar()
        {
            var fRemarcar = new RemarcarConsultaWin(consulta);
            fRemarcar.ShowDialog();

            if (fRemarcar.OK)
            {
                Removeu = true;
                Close();
            }
        }

        #region EVENTOS

        private void btnGerar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
                MantemItem((item as TreeViewItem)?.Uid);
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void TreeViewItem_OnItemSelected(object sender, RoutedEventArgs e) => item = sender;


        private void TreeViewItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MantemItem((item as TreeViewItem)?.Uid);
        }

        #endregion
    }
}
