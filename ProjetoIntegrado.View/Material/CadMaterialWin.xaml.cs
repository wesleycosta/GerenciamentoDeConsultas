using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Material
{
    using Model;
    using Funcoes;

    public partial class CadMaterialWin
    {
        #region  PROPRIEDADES E CTOR
        public bool cadastrou { get; private set; }

        private readonly MaterialModel material;
        private readonly bool cadastrar;

        public CadMaterialWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVO MATERIAL";
            cadastrar = true;
        }

        public CadMaterialWin(MaterialModel material)
        {
            InitializeComponent();
            Iniciar();
            this.material = material;

            Title = "EDITAR MATERIAL";
            CarregarDados();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            tbValor.KeyDown += ValidarEntrada.Real_KeyPress;
            tbValor.KeyDown += MetroWindow_KeyDown;

            Loaded += (o, a) => tbDescricao.Focus();
        }

        private void CarregarDados()
        {
            tbDescricao.Text = material.descricao;
            tbValor.Text = material.valor.ToString("n");
        }

        #endregion

        #region MANTEM MATERIAL

        private MaterialModel ToModel() =>
            new MaterialModel
            {
                id = material?.id ?? 0,
                descricao = tbDescricao.Text,
                valor = decimal.Parse(tbValor.Text)
            };

        private void MantemMaterial()
        {
            var material = ToModel();

            if (cadastrar)
                material.Cadastrar();
            else
                material.Atualizar();

        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemMaterial();
                cadastrou = true;
                Close();
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) =>
            Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion'
    }
}
