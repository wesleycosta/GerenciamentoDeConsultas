using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Pagamento
{
    using Model;
    using Funcoes;
    using System;

    public partial class CadPagamentoWin
    {
        #region  PROPRIEDADES E CTOR

        private List<FormaDePagamentoModel> listaFormaDePagamento;
        public bool cadastrou { get; private set; }

        private int idConsulta { get; set; }
        private PagamentoModel pagamento;
        private readonly bool cadastrar;

        public CadPagamentoWin(int idConsulta)
        {
            InitializeComponent();
            this.idConsulta = idConsulta;
            tbData.SelectedDate = DateTime.Now;
            Iniciar();

            Title = "NOVO PAGAMENTO";
            cadastrar = true;
        }

        public CadPagamentoWin(PagamentoModel pagamento, int idConsulta)
        {
            InitializeComponent();
            Iniciar();
            this.pagamento = pagamento;
            this.idConsulta = idConsulta;

            Title = "EDITAR PAGAMENTO";
            CarregarDados();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            tbQtdParcelas.KeyDown += ValidarEntrada.Naturais_KeyPress;
            tbValor.KeyDown += ValidarEntrada.Real_KeyPress;

            CarregarFormaDePagamento();
            Loaded += (o, a) => cbFormaDePagamento.Focus();
        }

        private void CarregarFormaDePagamento()
        {
            listaFormaDePagamento = FormaDePagamentoModel.CarregarTodos();
            cbFormaDePagamento.Items.Clear();
            listaFormaDePagamento.ForEach(item => cbFormaDePagamento.Items.Add(item.descricao));

            if (listaFormaDePagamento.Count > 0)
                cbFormaDePagamento.SelectedIndex = 0;
        }

        private void CarregarDados()
        {
            var index = listaFormaDePagamento.IndexOf(pagamento.formaDePagamento);

            if (index > 0)
                cbFormaDePagamento.SelectedIndex = index;

            tbData.SelectedDate = pagamento.data;
            tbQtdParcelas.Text = pagamento.qtdParcelas.ToString();
            tbValor.Text = pagamento.valor.ToString("n");
        }

        #endregion

        #region MANTEM PAGAMENTO

        private PagamentoModel ToModel() =>
            new PagamentoModel
            {
                id = pagamento?.id ?? 0,
                idConsulta = idConsulta,
                caixa = Sessao.caixa,
                formaDePagamento = listaFormaDePagamento[cbFormaDePagamento.SelectedIndex],
                data = tbData.SelectedDate.Value,
                qtdParcelas = int.Parse(tbQtdParcelas.Text),
                valor = decimal.Parse(tbValor.Text),
                ativo = true
            };

        private void MantemDados()
        {
            var pag = ToModel();

            if (cadastrar)
                pag.Cadastrar();
            else
                pag.Atualizar();
        }

        #endregion

        #region VALIDAR CAIXA

        private bool ValidarCaixa()
        {
            if (!Sessao.caixa.caixaAberto)
            {
                var f = new FluxoDeCaixa.AbrirCaixaWin();
                f.ShowDialog();

                return f.abriuCaixa;
            }

            return true;
        }


        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCaixa())
                if (ValidarCampos.Validar(this))
                {
                    MantemDados();
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

        #endregion
    }
}
