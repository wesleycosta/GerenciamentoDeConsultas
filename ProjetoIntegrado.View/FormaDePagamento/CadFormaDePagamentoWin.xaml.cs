using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.FormaDePagamento
{
    using Model;

    public partial class CadFormaDePagamentoWin
    {
        public bool cadastrou;

        private FormaDePagamentoModel formaDePagamento = new FormaDePagamentoModel();
        private bool cadastrar;

        public CadFormaDePagamentoWin()
        {
            InitializeComponent();
            Iniciar();

            Title = "NOVA FORMA DE PAGAMENTO";
            cadastrar = true;
        }

        public CadFormaDePagamentoWin(FormaDePagamentoModel formaDePagamento)
        {
            InitializeComponent();
            Iniciar();
            this.formaDePagamento = formaDePagamento;

            Title = "EDITAR FORMA DE PAGAMENTO";
            CarregarDados();
        }

        private void Iniciar()
        {
            Loaded += (o, a) => tbDescricao.Focus();
        }

        private void CarregarDados()
        {
            tbDescricao.Text = formaDePagamento.descricao;
        }

        #region MANTEM CARGO

        private FormaDePagamentoModel ToModel() =>
            new FormaDePagamentoModel
            {
                id = formaDePagamento.id,
                descricao = tbDescricao.Text
            };

        private void MantemFormaDePagamento()
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
            MantemFormaDePagamento();
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
