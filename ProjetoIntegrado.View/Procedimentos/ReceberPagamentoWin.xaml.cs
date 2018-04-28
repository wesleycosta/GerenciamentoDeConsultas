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

namespace ProjetoIntegrado.View.Procedimentos
{
    using Model;
    using Funcoes;

    public partial class ReceberPagamentoWin
    {
        public bool Ok;
        private List<ConsultaModel> listaDeConsultas = new List<ConsultaModel>();

        public ReceberPagamentoWin(List<ConsultaModel> listaDeConsultas)
        {
            InitializeComponent();
            this.listaDeConsultas = listaDeConsultas;
            CarregarDados();
        }

        private void CarregarDados()
        {
            lvwConsultas.ItemsSource = listaDeConsultas;
            lbTotalRegistro.Content = listaDeConsultas.Count.ToString("D3");
            lbTotal.Content = listaDeConsultas.Sum(x => x.valor).ToString("n");
        }

        public void ReceberConsultas()
        {
            foreach (var o in listaDeConsultas)
                o.ReceberPagamento();
        }

        #region EVENTOS

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            ReceberConsultas();
            Ok = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) => Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void lvwConsultas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lvwConsultas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}
