using System;
using System.Linq;
using System.Windows.Input;

namespace ProjetoIntegrado.View.FluxoDeCaixa
{
    using MahApps.Metro.Controls.Dialogs;
    using Mensagens;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public partial class PrincipalFluxoDeCaixaWin
    {
        private List<CaixaSaidaModel> lSaidas = new List<CaixaSaidaModel>();

        private double totalEntrada;
        private double totalSaida;
        private double totalSaldo;

        public PrincipalFluxoDeCaixaWin()
        {
            InitializeComponent();
            Iniciar();
        }

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            var caixaAberto = Sessao.caixa?.caixaAberto ?? false;

            BtnAbrirCaixa.IsEnabled = !caixaAberto;
            BtnSaida.IsEnabled = BtnFecharCaixa.IsEnabled = caixaAberto;

            if (caixaAberto)
                CarregarDados();
        }

        private void CarregarDados()
        {
            CarregarEntradas();
            CarregarSaidas();

            totalSaldo = totalEntrada - totalSaida;
            CarregarLabel();
        }

        private void CarregarEntradas()
        {

        }

        private void CarregarSaidas()
        {
            if (Sessao.caixa?.caixaAberto ?? false)
            {
                lSaidas = Sessao.caixa.CarregarSaidas();
                lvwSaidas.ItemsSource = lSaidas;
                totalSaida = lSaidas.Sum(x => (double)x.valor);
            }
        }

        private void CarregarLabel()
        {
            var dtAbertura = Sessao.caixa.dtAbertura;
            var funcionario = Sessao.caixa.funcionarioAbertura.nome;
            lbAbertura.Content = $"{dtAbertura.ToShortDateString()} às {dtAbertura.ToShortTimeString()}";

            lbFuncionario.Content = funcionario;
            lbValorInicial.Content = Sessao.caixa.valorInicial.ToString("n");
            lbTotal.Content = totalEntrada.ToString("n");
            lbSaida.Content = totalSaida.ToString("n");
            lbSaldo.Content = totalSaldo.ToString("n");
        }

        #endregion

        #region EVENTOS

        private void BtnAbrir_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var frmAbrir = new AbrirCaixaWin();
            frmAbrir.ShowDialog();

            if (frmAbrir.abriuCaixa)
                Iniciar();
        }

        private void BtnFechar_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var frmFechar = new FecharCaixaWin();
            frmFechar.ShowDialog();

            if (frmFechar.fechouCaixa)
                Iniciar();
        }

        private void BtnSaida_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fSaida = new LancarSaidaWin();
            fSaida.ShowDialog();

            if (fSaida.cadastrou)
                CarregarDados();
        }

        private void lvwSaidas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvwSaidas.SelectedIndex >= 0)
            {
                var caixa = lvwSaidas.SelectedItems[0] as CaixaSaidaModel;
                var cadLancar = new LancarSaidaWin(caixa);
                cadLancar.ShowDialog();

                if (cadLancar.cadastrou)
                    CarregarDados();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Delete)
                if ((sender as ListView) == lvwSaidas)
                    RemoverSaida();
        }

        private void RemoverSaida()
        {
            if (lvwSaidas.SelectedIndex >= 0)
            {
                var r = Mbox.DesejaExcluir();

                if (r == MessageDialogResult.Affirmative)
                {
                    var saida = lvwSaidas.SelectedItems[0] as CaixaSaidaModel;
                    saida?.Remover();

                    CarregarDados();
                }
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #endregion
    }
}
