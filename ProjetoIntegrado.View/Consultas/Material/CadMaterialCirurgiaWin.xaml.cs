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

namespace ProjetoIntegrado.View.Consultas.Material
{
    using Model;

    public partial class CadMaterialCirurgiaWin
    {
        public bool cadastrou { get; set; }
        private readonly CirurgiaModel cirurgia;

        public CadMaterialCirurgiaWin(CirurgiaModel cirurgia)
        {
            this.cirurgia = cirurgia;
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
