using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Cargo
{
    using Model;

    public partial class CadCargoWin
    {
        public bool cadastrou;

        private CargoModel cargo = new CargoModel();
        private bool cadastrar;

        public CadCargoWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVO CARGO";
            cadastrar = true;
        }

        public CadCargoWin(CargoModel cargo)
        {
            InitializeComponent();
            Iniciar();
            this.cargo = cargo;

            Title = "EDITAR CARGO";
            CarregarDados();
        }

        private void Iniciar()
        {
            Loaded += (o, a) => tbDescricao.Focus();
        }

        private void CarregarDados()
        {
            tbDescricao.Text = cargo.descricao;
        }

        #region MANTEM CARGO

        private CargoModel ToModel() =>
            new CargoModel
            {
                id = cargo.id,
                descricao = tbDescricao.Text
            };

        private void MantemCargo()
        {
            var cargo = ToModel();

            if (cadastrar)
                cargo.Cadastrar();
            else
                cargo.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            MantemCargo();
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
