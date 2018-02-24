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

    public partial class FecharCaixaWin
    {
        internal bool fechouCaixa;

        public FecharCaixaWin()
        {
            InitializeComponent();
        }


        #region EVENTOS

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            fechouCaixa = true;
            Close();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
