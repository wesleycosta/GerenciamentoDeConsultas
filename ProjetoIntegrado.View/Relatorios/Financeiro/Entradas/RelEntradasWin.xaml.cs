using System;

namespace ProjetoIntegrado.View.Relatorios.Financeiro.Entradas
{
    using ViewUtil;
    using DataSets;

    public partial class RelEntradasWin 
    {
        private DateTime dtInicial;
        private DateTime dtFinal;

        public RelEntradasWin(DateTime dtInicial, DateTime dtFinal)
        {
            this.dtInicial = dtInicial;
            this.dtFinal = dtFinal;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            Carregar();
        }

        public void Carregar()
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Financeiro.Entradas.rptEntradas.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportFechamento(dtInicial, dtFinal));
            rptViewer.LocalReport.SetParameters(ControleDataSets.GetParametros(dtInicial, dtFinal));

            rptViewer.RefreshReport();
        }
    }
}