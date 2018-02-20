using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Cargo
{
    using Model;
    using Funcoes;

    public partial class CadCargoWin
    {
        #region  PROPRIEDADES E CTOR
        public bool cadastrou { get; private set; }

        private readonly CargoModel cargo;
        private readonly bool cadastrar;

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

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar() => Loaded += (o, a) => tbDescricao.Focus();

        private void CarregarDados()
        {
            tbDescricao.Text = cargo.descricao;
        }

        #endregion

        #region MANTEM CARGO

        private CargoModel ToModel() =>
            new CargoModel
            {
                id = cargo?.id ?? 0,
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
            if (ValidarCampos.Validar(this))
            {
                MantemCargo();
                cadastrou = true;
                Close();
            }
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
