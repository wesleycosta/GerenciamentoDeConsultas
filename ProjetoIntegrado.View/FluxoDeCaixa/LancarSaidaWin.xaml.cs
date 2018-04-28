using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;


namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    using Funcoes;
    using Model;

    public partial class LancarSaidaWin
    {
        public bool cadastrou;

        private bool cadastrar;
        private CaixaSaidaModel caixa;

        public LancarSaidaWin()
        {
            InitializeComponent();
            Iniciar();
            cadastrar = true;
        }

        public LancarSaidaWin(CaixaSaidaModel caixa)
        {
            this.caixa = caixa;

            InitializeComponent();
            Iniciar();
            CarregarDados();
        }

        #region CARREGAR DADOS E INICIAR

        private void Iniciar()
        {
            tbValor.KeyDown += ValidarEntrada.Real_KeyPress;
            Loaded += (o, a) => tbDescricao.Focus();
        }

        private void CarregarDados()
        {
            tbDescricao.Text = caixa.descricao;
            tbValor.Text = caixa.valor.ToString("n");
        }

        #endregion

        #region MANTEM CAIXA SAIDA

        private CaixaSaidaModel ToModel() =>
            new CaixaSaidaModel
            {
                id = caixa?.id ?? 0,
                idCaixa = Sessao.caixa.id,
                descricao = tbDescricao.Text,
                valor = decimal.Parse(tbValor.Text),
                ativo = true
            };

        private void MantemDados()
        {
            var caixa = ToModel();

            if (cadastrar)
                caixa.Cadastrar();
            else
                caixa.Atualizar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemDados();
                CaixaControle.CarregarSessao();
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
