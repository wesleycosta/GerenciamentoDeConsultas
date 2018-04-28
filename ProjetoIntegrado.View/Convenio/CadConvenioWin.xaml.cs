using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Convenio
{
    using Model;
    using Funcoes;

    public partial class CadConvenioWin
    {
        #region  PROPRIEDADES E CTOR
        public bool cadastrou { get; private set; }

        private readonly ConvenioModel convenio;
        private readonly bool cadastrar;

        public CadConvenioWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVO CONVÊNIO";
            cadastrar = true;
        }

        public CadConvenioWin(ConvenioModel cargo)
        {
            InitializeComponent();
            Iniciar();
            this.convenio = cargo;

            Title = "EDITAR CONVÊNIO";
            CarregarDados();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar() => Loaded += (o, a) => tbDescricao.Focus();

        private void CarregarDados()
        {
            tbDescricao.Text = convenio.nome;
        }

        #endregion

        #region MANTEM CONVÊNIO

        private ConvenioModel ToModel() =>
            new ConvenioModel
            {
                id = convenio?.id ?? 0,
                nome = tbDescricao.Text
            };

        private void MantemConvenio()
        {
            var convenio = ToModel();

            if (cadastrar)
                convenio.Cadastrar();
            else
                convenio.Atualizar();

        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemConvenio();
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
