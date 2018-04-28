using System;

namespace ProjetoIntegrado.View.Relatorios.Financeiro.Despesas
{
    using ViewUtil;
    using DataSets;

    public partial class RelDespesaWin 
    {
        private DateTime dtInicial;
        private DateTime dtFinal;

        public RelDespesaWin(DateTime dtInicial, DateTime dtFinal)
        {
            this.dtInicial = dtInicial;
            this.dtFinal = dtFinal;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Financeiro.Despesas.rptDespesas.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportDespesas(dtInicial, dtFinal));
            rptViewer.LocalReport.SetParameters(ControleDataSets.GetParametros(dtInicial, dtFinal));

            rptViewer.RefreshReport();
        }
    }
}
