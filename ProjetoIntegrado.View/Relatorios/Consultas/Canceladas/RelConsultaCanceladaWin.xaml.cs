using System;

namespace ProjetoIntegrado.View.Relatorios.Consultas.Canceladas
{
    using ViewUtil;
    using DataSets;

    public partial class RelConsultaCanceladaWin
    {
        private DateTime dtInicial;
        private DateTime dtFinal;

        public RelConsultaCanceladaWin(DateTime dtInicial, DateTime dtFinal)
        {
            this.dtInicial = dtInicial;
            this.dtFinal = dtFinal;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Consultas.Canceladas.rptConsultasCanceladas.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportConsultaCancelada(dtInicial, dtFinal));
            rptViewer.LocalReport.SetParameters(ControleDataSets.GetParametros(dtInicial, dtFinal));

            rptViewer.RefreshReport();
        }
    }
}
