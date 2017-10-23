using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Relatorios.Filtros
{
    public partial class IntervaloDataWin
    {
        public bool SelecionouOK { get; private set; }

        public IntervaloDataWin()
        {
            InitializeComponent();
        }

        #region EVENTOS

        private void BtnOk_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            SelecionouOK = true;
            Close();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) => Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
