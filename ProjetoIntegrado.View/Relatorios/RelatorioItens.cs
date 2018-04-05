using System;
using System.Windows.Threading;

namespace ProjetoIntegrado.View.Relatorios
{
    using Funcoes;
    using Consultas.ListaConsultas;
    using Consultas.Canceladas;
    using Consultas.Procedimentos;


    using Financeiro.Despesas;
    using Financeiro.Faturamento;

    public class RelatorioItens
    {
        #region  METODOS ITEM

        public static void MantemItem(string itemSelecionado)
        {
            try
            {
                var item = (RelatorioEnum)Enum.Parse(typeof(RelatorioEnum), itemSelecionado);

                switch (item)
                {
                    case RelatorioEnum.ListaDeConsultas: ListaDeConsultas(); break;
                    case RelatorioEnum.ConsultasCanceladas: ConsultasCanceladas(); break;
                    case RelatorioEnum.Procedimentos: Procedimentos(); break;


                    case RelatorioEnum.Despesas: Despesas(); break;
                    case RelatorioEnum.Faturamento: Faturamento(); break;
                }
            }
            catch (Exception) { }
        }

        #endregion

        #region CONSULTAS

        private static void ListaDeConsultas()
        {
            var frmData = new Filtros.FiltroOftalWin();
            frmData.ShowDialog();

            if (frmData.SelecionouOK)
            {
                SplashScreenControle.Mostrar();

                var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                     (o, a) =>
                                     {
                                         new RelListaConsultasWin(frmData.dtInicial, frmData.dtFinal, frmData.medico).ShowDialog();
                                         var trm = o as DispatcherTimer;
                                         trm.IsEnabled = false;
                                     }
                                     );
            }
        }

        private static void Procedimentos()
        {
            var frmData = new Filtros.FiltroConvenioWin();
            frmData.ShowDialog();

            if (frmData.SelecionouOK)
            {
                SplashScreenControle.Mostrar();

                var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                     (o, a) =>
                                     {
                                         new RelProcedimentosWin(frmData.dtInicial,
                                                                 frmData.dtFinal,
                                                                 frmData.convenio,
                                                                 frmData.status).ShowDialog();
                                         var trm = o as DispatcherTimer;
                                         trm.IsEnabled = false;
                                     }
                                     );
            }
        }

        private static void ConsultasCanceladas()
        {
            var frmData = new Filtros.IntervaloDataWin();
            frmData.ShowDialog();

            if (frmData.SelecionouOK)
            {
                SplashScreenControle.Mostrar();

                var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                     (o, a) =>
                                        {
                                            new RelConsultaCanceladaWin(frmData.dtInicial, frmData.dtFinal).ShowDialog();
                                            var trm = o as DispatcherTimer;
                                            trm.IsEnabled = false;
                                        }
                                     );
            }
        }

        #endregion

        #region FINANCEIRO

        private static void Faturamento()
        {
            var frmData = new Filtros.IntervaloDataWin();
            frmData.ShowDialog();

            if (frmData.SelecionouOK)
            {
                SplashScreenControle.Mostrar();

                var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                     (o, a) =>
                                     {
                                         new RelFaturamentoWin(frmData.dtInicial, frmData.dtFinal).ShowDialog();
                                         var trm = o as DispatcherTimer;
                                         trm.IsEnabled = false;
                                     });
            }
        }

        private static void Despesas()
        {
            var frmData = new Filtros.IntervaloDataWin();
            frmData.ShowDialog();

            if (frmData.SelecionouOK)
            {
                SplashScreenControle.Mostrar();

                var x = new TimerUtil(TimeSpan.FromMilliseconds(300),
                                     (o, a) =>
                                     {
                                         new RelDespesaWin(frmData.dtInicial, frmData.dtFinal).ShowDialog();
                                         var trm = o as DispatcherTimer;
                                         trm.IsEnabled = false;
                                     });
            }
        }

        #endregion
    }
}
