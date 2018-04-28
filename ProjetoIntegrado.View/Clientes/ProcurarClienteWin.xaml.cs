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

namespace ProjetoIntegrado.View.Clientes
{
    using Model;
    using Funcoes;
    using Mensagens;
    using ViewUtil;

    public partial class ProcurarClienteWin
    {
        private List<ClienteModel> lClientes;

        public ClienteModel Cliente { get; set; }
        public bool Vinculou { get; set; }

        public ProcurarClienteWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarClientes();
            lvwClientes.MouseDoubleClick += (o, a) => Vincular();
        }

        #region CARREGAR DADOS

        private FiltroPessoa GetFiltro() =>
          (FiltroPessoa)Enum.Parse(typeof(FiltroPessoa), Mascara.Remover(cbFiltro.Text.ToLower()));

        private void CarregarClientes()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lClientes = ClienteModel.CarregarTodos();
            else
                lClientes = ClienteModel.Pesquisar(GetFiltro(), tbPesquisa.Text);

            lvwClientes.ItemsSource = lClientes;
            tbPesquisa.Focus();
        }

        #endregion

        #region VINCULAR

        private void Vincular()
        {
            if (lvwClientes.SelectedItems.Count > 0)
            {
                Cliente = lClientes[lvwClientes.SelectedIndex];
                Vinculou = true;
                Close();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Enter)
                Vincular();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarClientes();
        }

        private void cbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        //=> CarregarClientes();

        #endregion
    }
}
