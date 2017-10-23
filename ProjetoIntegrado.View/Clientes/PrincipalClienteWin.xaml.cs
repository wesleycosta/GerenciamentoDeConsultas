using System.Windows.Input;

namespace ProjetoIntegrado.View.Clientes
{
    public partial class PrincipalClienteWin 
    {
        public PrincipalClienteWin()
        {
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }
    }
}
