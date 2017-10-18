using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace ProjetoIntegrado.View.Categoria
{
    using Model;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalCategoriaWin 
    {
        List<CategoriaModel> categorias = new List<CategoriaModel>();

        public PrincipalCategoriaWin()
        {
            InitializeComponent();

            Loaded += (o, a) => CarregarCategorias();
        }

        #region MANTEM CARGO

        private void CarregarCategorias()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                categorias = CategoriaModel.CarregarTodos();
            else
                categorias = CategoriaModel.Pesquisar(tbPesquisa.Text);

            lvwCategorias.ItemsSource = categorias;
            tbPesquisa.Focus();
        }

        private void Novo()
        {
            var cadCategoria = new CadCategoriaWin();
            cadCategoria.ShowDialog();

            if (cadCategoria.cadastrou)
                CarregarCategorias();
        }

        private void Editar()
        {
            if (lvwCategorias.SelectedIndex >= 0)
            {
                var categoria = lvwCategorias.SelectedItems[0] as CategoriaModel;
                var cadCategoria = new CadCategoriaWin(categoria);
                cadCategoria.ShowDialog();

                if (cadCategoria.cadastrou)
                    CarregarCategorias();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void Remover()
        {
            if (lvwCategorias.SelectedIndex >= 0)
            {
                var categoria = lvwCategorias.SelectedItems[0] as CategoriaModel;
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    categoria.Remover();
                    categorias.Remove(categoria);
                    lvwCategorias.Items.Refresh();
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
            CarregarCategorias();

            if (e.Key == Key.Down)
                lvwCategorias.SelecionarPrimeiraLinha();
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
