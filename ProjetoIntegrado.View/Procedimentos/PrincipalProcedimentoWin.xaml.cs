using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace ProjetoIntegrado.View.Procedimentos
{
    using Model;
    using Funcoes;
    using Mensagens;

    public partial class PrincipalProcedimentoWin
    {
        private List<ConsultaModel> listaDeConsultas = new List<ConsultaModel>();
        private List<ConvenioModel> listaDeConvenios = new List<ConvenioModel>();

        public PrincipalProcedimentoWin()
        {
            InitializeComponent();
            CarregarConvenios();
            Pesquisar();
        }

        #region CARREGAR DADOS

        private void CarregarConvenios()
        {
            cbConvenio.Items.Clear();
            cbConvenio.Items.Add("TODOS");
            listaDeConvenios = ConvenioModel.CarregarTodos();

            listaDeConvenios.ForEach(x => cbConvenio.Items.Add(x.nome));
        }

        private void Pesquisar()
        {
            listaDeConsultas = ConsultaModel.Procedimentos(GetConvenio(), GetFiltro(), tbPesquisa.Text);
            lbTotalRegistro.Content = listaDeConsultas.Count.ToString("D3");
            lvwFuncionarios.ItemsSource = listaDeConsultas;
            lbTotal.Content = listaDeConsultas.Sum(x => x.valor).ToString("n");
        }

        private FiltroPessoa GetFiltro() =>
                (FiltroPessoa)Enum.Parse(typeof(FiltroPessoa), Mascara.Remover((cbFiltro.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower()));

        private ConvenioModel GetConvenio() =>
            cbConvenio.SelectedIndex == 0 ? null : listaDeConvenios[cbConvenio.SelectedIndex - 1];


        #endregion

        private void Abrir()
        {
            if (lvwFuncionarios.SelectedIndex >= 0)
            {
                var consultas = new List<ConsultaModel>();

                foreach (var o in lvwFuncionarios.SelectedItems)
                    consultas.Add(o as ConsultaModel);

                var frmReceber = new ReceberPagamentoWin(consultas);
                frmReceber.ShowDialog();

                if (frmReceber.Ok)
                    Pesquisar();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #region EVENTOS

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void lvwFuncionarios_MouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            Abrir();

        private void lvwFuncionarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e) =>
            Pesquisar();

        private void cbConvenio_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            Pesquisar();

        private void BtnBaixa_Click(object sender, RoutedEventArgs e) =>
            Abrir();

        #endregion
    }
}
