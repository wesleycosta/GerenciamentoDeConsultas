using System;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace ProjetoIntegrado.View.SplashInicial
{
    using Funcoes;
    using BaseDeDados;
    using View;

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

        private void Executar(Action acao) =>
                            Dispatcher.Invoke(acao);

        private void SetStatus(string status) =>
                            Executar(() => lbStatus.Text = status);

        private void Carregar()
        {
            try
            {
                CarregarConfiguracao();
                MantemBaseDeDados();
                CarregarComponentes();
                MantemLogin();
            }
            catch (Exception ex)
            {
                Excecao.Mostrar(ex);
                Processo.MatarProcessoSistema();
            }
        }

        private void CarregarConfiguracao()
        {
            SetStatus("Carregando configurações...");
            var carregou = Conexao.Iniciar();

            if (!carregou)
                throw new Exception("Não possível carregar o arquivo de configuração");
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

        private void CarregarComponentes()
        {
            SetStatus("Carregando componentes...");
        }

        private void MantemLogin()
        {
            Executar(() => new Login.LoginWin(this).ShowDialog());
        }
    }
}
