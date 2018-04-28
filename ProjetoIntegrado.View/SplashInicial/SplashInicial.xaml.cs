using System;
using System.Windows.Threading;
using System.Threading.Tasks;
using ProjetoIntegrado.View.Properties;

namespace ProjetoIntegrado.View.SplashInicial
{
    using Funcoes;
    using BaseDeDados;
    using System.Globalization;
    using System.Threading;

    public partial class SplashInicial
    {
        #region CTOR E LOAD 

        public SplashInicial()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var timer = new TimerUtil(TimeSpan.FromSeconds(1),
                                     async (obj, args) =>
                                     {
                                         var trm = obj as DispatcherTimer;
                                         trm.IsEnabled = false;
                                         trm.Stop();

                                         await Task.Run(() => Carregar());
                                     });

            timer.Iniciar();
        }

        #endregion

        private void Carregar()
        {
            try
            {
                CarregarConfiguracao();     // LÊ OS DADOS DO ARQUIVO E CONFIGURA A STRING DE CONEXÃO
                CarregarComponentes();      // CARREGA OS COMPONENTES DO SISTEMA
                MantemBaseDeDados();        // VERIFICA  SE O BANCO DE DADOS EXISTE, CASO NÃO CRIA A BASE DE DADOS E AS TABELAS
                MantemCadastroEmpresa();    // VERIFICAR SE EXISTE O CADASTRO DE EMPRESA, CASO NÃO APRESENTA A TELA DE CADASTRO
                MantemLogin();              // INICIALIZA O LOGIN
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
                Processo.MatarProcessoSistema();
            }
        }

        private void MantemLogin()
        {
            Executar(() => new Login.LoginWin(this).ShowDialog());
        }

        #region TRATAMENTO THREAD

        private void Executar(Action acao) =>
                            Dispatcher.Invoke(acao);

        private void SetStatus(string status) =>
                            Executar(() => lbStatus.Text = status);

        #endregion

        #region METODOS DE CARREGAMENTO

        private void CarregarConfiguracao()
        {
            SetStatus("Carregando configurações...");
            var carregou = Conexao.Iniciar();

            if (!carregou)
                throw new Exception("Não possível carregar o arquivo de configuração");

            Settings.Default["Conexao"] = Conexao.StringDeConexao;
        }


        private void CarregarRelatorio()
        {
            Dispatcher.Invoke(() =>
            {
                var x = new Relatorios.Financeiro.Faturamento.RelFaturamentoWin(DateTime.Now, DateTime.Now);
                x.Carregar();
            });
        }

        private void MantemBaseDeDados()
        {
            SetStatus("Verificando se a base de dados existe...");

            if (!BancoDeDados.ExisteBaseDeDados())
            {
                SetStatus("Criando base de dados...");
                BancoDeDados.CriarBaseDeDados();
            }
        }

        private void MantemCadastroEmpresa()
        {
            SetStatus("Verificando se existe o cadastro da empresa...");

            if (!Model.ClinicaModel.ExisteCadastro())
                Executar(() =>
                {
                    var frmCad = new Clinica.CadClinicaWin(true);
                    frmCad.ShowDialog();

                    // SE CANCELAR O CADASTRO DA CLINICA, FECHA A APLICAÇÃO
                    if (!frmCad.cadastrou)
                        Processo.MatarProcessoSistema();
                });
        }

        private void CarregarComponentes()
        {
            SetStatus("Carregando componentes...");
            new ViewUtil.AbrirArquivo();
        }

        #endregion
    }
}
