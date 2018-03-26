using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using ProjetoIntegrado.Funcoes;

namespace ProjetoIntegrado.View.Principal
{
    using Model;
    using View;
    using ViewUtil;
    using Mensagens;
    using System.Collections.Generic;

    public partial class JanelaPrincipalWin
    {
        private bool iniciou;
        private readonly Window frmLogin;
        private List<FuncionarioModel> listaDeMedicos = new List<FuncionarioModel>();
        private List<ConsultaModel> listaDeConsultas = new List<ConsultaModel>();

        public JanelaPrincipalWin(Window frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
            MenuItens.Evento = EventoItem;
            tbDataInicial.SelectedDate = tbDataFinal.SelectedDate = DateTime.Now.Date;

            IniciarIcones();
            CarregarUsuario();

            Closing += (o, a) =>
                a.Cancel = !Sair();

            ValidarCampos.JanelaPrincipal = this;
        }

        private bool Sair()
        {
            var r = Mbox.DesejaSair();
            return r == MessageDialogResult.Affirmative;
        }

        #region CARREGAR DADOS

        private void CarregarUsuario() => tbUsuario.Text = Sessao.funcionario.usuario;

        private void CarregarDados()
        {
            try
            {

                listaDeConsultas = ConsultaModel.Pesquisar(GetFiltro(),
                                                           tbPesquisa.Text,
                                                           GetAtendimento(),
                                                           tbDataInicial.SelectedDate.Value,
                                                           tbDataFinal.SelectedDate.Value,
                                                           cbMedicos.SelectedIndex > 0 ? listaDeMedicos[cbMedicos.SelectedIndex - 1] : null,
                                                           null);

                lvwConsultas.ItemsSource = listaDeConsultas;
                tbPesquisa.Focus();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
            }
        }

        private FiltroPessoa GetFiltro() =>
                (FiltroPessoa)Enum.Parse(typeof(FiltroPessoa), Mascara.Remover((cbFiltro.SelectedItem as ComboBoxItem)?.Content.ToString().ToLower()));

        private FormaDeAtendimento GetAtendimento()
        {
            if (cbTipo.SelectedIndex == 1)
                return FormaDeAtendimento.Particular;
            else if (cbTipo.SelectedIndex == 2)
                return FormaDeAtendimento.Convenio;

            return FormaDeAtendimento.Todos;
        }

        private void IniciarIcones()
        {
            // LOGO
            imgLogo.BitmapToImageSource(Icons.eyeglasses);

            // PRINCIPAL
            imgPacientes.BitmapToImageSource(Icons.Customer_16x16);
            imgProcedimentos.BitmapToImageSource(Icons.Time_16x16);

            // CADASTROS
            imgEmpresa.BitmapToImageSource(Icons.Home_16x16);
            imgFuncionarios.BitmapToImageSource(Icons.Employee_16x16);
            imgUsuarios.BitmapToImageSource(Icons.Team_16x16);
            imgCargos.BitmapToImageSource(Icons.PackageProduct_16x16);
            imgConvenio.BitmapToImageSource(Icons.Contact_16x16);
            imgMaterial.BitmapToImageSource(Icons.AlignHorizontalTop_16x16);
            // FINANCEIRO
            imgFormaDePagamento.BitmapToImageSource(Icons.FullStackedBar_16x16);
            imgFluxoDeCaixa.BitmapToImageSource(Icons.FluxoDeCaixa16x16);
            imgDespesas.BitmapToImageSource(Icons.SwitchTimeScalesTo_16x16);

            // RELATORIOS E CONFIGURACOES
            imgRelatorios.BitmapToImageSource(Icons.Report_16x16);
            imgTrocarUsuario.BitmapToImageSource(Icons.Project_16x16);
        }

        private void CarregarMedicos()
        {
            cbMedicos.Items.Clear();
            listaDeMedicos = FuncionarioModel.CarregarMedicos();
            cbMedicos.Items.Add("TODOS");
            listaDeMedicos.ForEach(x => cbMedicos.Items.Add(x.nome));
        }

        #endregion

        private void ApagarConsulta()
        {
            if (lvwConsultas.SelectedItems.Count > 0)
            {
                var item = lvwConsultas.SelectedItems[0] as ConsultaModel;
                var frmCancelar = new Consultas.ExcluirConsultaWin(item);
                frmCancelar.ShowDialog();

                if (frmCancelar.Removeu)
                    CarregarDados();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        #region EVENTOS

        // ON LOADED
        private void JanelaPrincipal_OnLoaded(object sender, RoutedEventArgs e)
        {
            frmLogin.Close();
            Mbox.JanelaPrincipal = this;

            CarregarMedicos();
            CarregarDados();
            iniciou = true;
        }

        // DOUBLE CLICK ITEM MENU
        private void Menu_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            MenuItens.MantemItem((sender as TreeViewItem)?.Uid);

        private void EventoItem(object sender, EventArgs e)
        {
            var item = MenuItens.GetItem(sender.ToString());

            if (item == MenuItensEnum.TrocarUsuario)
                CarregarUsuario();
            else if (item == MenuItensEnum.Funcionarios)
            {
                CarregarMedicos();
                cbMedicos.SelectedIndex = 0;
            }
        }

        // KEY DOWN
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if (iniciou)
                CarregarDados();
        }

        private void cbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iniciou)
                CarregarDados();
        }

        private void tbDataInicial_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iniciou)
                CarregarDados();
        }
        private void lvwConsultas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                ApagarConsulta();
        }

        #endregion

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            var frmConsulta = new Consultas.CadConsultasWin();
            frmConsulta.ShowDialog();

            if (frmConsulta.cadastrou)
                CarregarDados();
        }

        private void lvwConsultas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvwConsultas.SelectedIndex >= 0)
            {
                var consulta = lvwConsultas.SelectedItems[0] as ConsultaModel;
                var cadConsuta = new Consultas.CadConsultasWin(consulta);

                cadConsuta.ShowDialog();

                if (cadConsuta.cadastrou)
                    CarregarDados();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }
    }
}
