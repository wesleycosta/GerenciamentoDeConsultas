using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Relatorios.Filtros
{
    using Funcoes;

    public partial class IntervaloDataWin
    {
        public bool SelecionouOK { get; private set; }

        public IntervaloDataWin()
        {
            InitializeComponent();


            tbDataInicial.SelectedDate = DataUtil.GetPrimeiroDiaDesseMes();
            tbDataFinal.SelectedDate = DataUtil.GetUltimoDiaDesseMes();
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
