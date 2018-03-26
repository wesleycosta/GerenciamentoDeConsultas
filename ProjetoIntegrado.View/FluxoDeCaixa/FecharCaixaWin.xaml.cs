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

namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    using Funcoes;
    using Model;

    internal class FecharCaixaListViewItem
    {
        public string formaDePagamento { get; set; }
        public double valor { get; set; }
    }

    public partial class FecharCaixaWin
    {
        public bool fechouCaixa;

        public FecharCaixaWin()
        {
            InitializeComponent();
            tbDiferenca.KeyDown += ValidarEntrada.Real_KeyPress;
            tbDiferenca.KeyDown += MetroWindow_KeyDown;
            CarregarListView();
            Loaded += (o, a) => tbDiferenca.Focus();
        }

        private void CarregarListView()
        {
            var lista = Sessao.caixa.CarregarTotalEntrada();
            lvwEntrada.Items.Clear();

            foreach (var item in lista)
                lvwEntrada.Items.Add(new FecharCaixaListViewItem
                {
                    formaDePagamento = item.formaDePagamento.descricao,
                    valor = (double)item.valor
                });
        }


        #region EVENTOS

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                fechouCaixa = true;
                Sessao.caixa.FecharCaixa(decimal.Parse(tbDiferenca.Text));
                Close();
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Enter && sender is TextBox)
                BtnSalvar_Click(sender, new RoutedEventArgs());
        }

        #endregion
    }
}
