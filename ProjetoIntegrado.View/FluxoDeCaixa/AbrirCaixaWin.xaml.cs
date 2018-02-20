using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    using Model;
    using Funcoes;
    using System;

    public partial class AbrirCaixaWin
    {
        public bool abriuCaixa { get; set; }

        public AbrirCaixaWin()
        {
            InitializeComponent();
            tbValorInicial.KeyDown += ValidarEntrada.Real_KeyPress;
        }

        #region MANTEM CAIXA

        private CaixaModel ToModel() =>
            new CaixaModel
            {
                valorInicial = decimal.Parse(tbValorInicial.Text),
                dtAbertura = DateTime.Now,
                funcionarioAbertura = Sessao.funcionario,
                ativo = true
            };

        private void MantemDados()
        {
            var caixa = ToModel();
            caixa.Cadastrar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemDados();
                CaixaControle.CarregarSessao();
                abriuCaixa = true;
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
