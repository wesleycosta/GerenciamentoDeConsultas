using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Cargo
{
    using Model;

    public partial class PrincipalCargoWin
    {
        List<CargoModel> cargos = new List<CargoModel>();

        public PrincipalCargoWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarCargos();
        }

        #region MANTEM CARGO

        private void CarregarCargos()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                cargos = CargoModel.CarregarTodos();
            else
                cargos = CargoModel.Pesquisar(tbPesquisa.Text);

            lvwCargos.ItemsSource = cargos;
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadCargo = new CadCargoWin();
            cadCargo.ShowDialog();

            if (cadCargo.cadastrou)
                CarregarCargos();
        }

        private void Editar()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var cargo = lvwCargos.SelectedItems[0] as CargoModel;
                var cadCargo = new CadCargoWin(cargo);
                cadCargo.ShowDialog();

                if (cadCargo.cadastrou)
                    CarregarCargos();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var cargo = lvwCargos.SelectedItems[0] as CargoModel;
                    cargo?.Remover();

                    cargos.Remove(cargo);
                    lvwCargos.Items.Refresh();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion

        #region EVENTOS

        private void BtnNovo_OnClick(object sender, RoutedEventArgs e)
        {
            Novo();
        }

        private void lvwCargos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarCargos();

            if (e.Key == Key.Down)
                lvwCargos.SelecionarPrimeiraLinha();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void lvwCargos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
            else if (e.Key == Key.Delete)
                Remover();
        }

        #endregion
    }
}
