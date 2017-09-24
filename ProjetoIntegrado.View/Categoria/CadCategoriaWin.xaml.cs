using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Categoria
{
    using Model;

    public partial class CadCategoriaWin
    {
        public bool cadastrou;

        private CategoriaModel categoria = new CategoriaModel();
        private bool cadastrar;

        public CadCategoriaWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVA CATEGORIA";
            cadastrar = true;
        }

        public CadCategoriaWin(CategoriaModel categoria)
        {
            InitializeComponent();
            Iniciar();
            this.categoria = categoria;

            Title = "EDITAR CATEGORIA";
            CarregarDados();
        }

        private void Iniciar()
        {
            Loaded += (o, a) => tbDescricao.Focus();
        }

        private void CarregarDados()
        {
            tbDescricao.Text = categoria.descricao;
        }

        #region MANTEM CARGO

        private CategoriaModel ToModel() =>
            new CategoriaModel
            {
                id = categoria.id,
                descricao = tbDescricao.Text
            };

        private void MantemCategoria()
        {
            var categoria = ToModel();

            if (cadastrar)
                categoria.Cadastrar();
            else
                categoria.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            MantemCategoria();
            cadastrou = true;
            Close();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
