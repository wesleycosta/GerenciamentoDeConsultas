using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Material
{
    using Model;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalMaterialWin 
    {
        private List<MaterialModel> materiais;

        public PrincipalMaterialWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarMateriais();
        }

        #region MANTEM MATERIAL

        private void CarregarMateriais()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                materiais = MaterialModel.CarregarTodos();
            else
                materiais = MaterialModel.Pesquisar(tbPesquisa.Text);

            lvwCargos.ItemsSource = materiais;
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadCargo = new CadMaterialWin();
            cadCargo.ShowDialog();

            if (cadCargo.cadastrou)
                CarregarMateriais();
        }

        private void Editar()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var cargo = lvwCargos.SelectedItems[0] as MaterialModel;
                var cadCargo = new CadMaterialWin(cargo);
                cadCargo.ShowDialog();

                if (cadCargo.cadastrou)
                    CarregarMateriais();
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
                    var material = lvwCargos.SelectedItems[0] as MaterialModel;
                    material?.Remover();

                    materiais.Remove(material);
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
            CarregarMateriais();

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
