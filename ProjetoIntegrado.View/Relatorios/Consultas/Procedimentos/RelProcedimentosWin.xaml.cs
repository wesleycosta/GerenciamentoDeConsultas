using System;

namespace ProjetoIntegrado.View.Relatorios.Consultas.Procedimentos
{
    using ViewUtil;
    using DataSets;
    using Model;

    public partial class RelProcedimentosWin 
    {
        private DateTime dtInicial;
        private DateTime dtFinal;
        private ConvenioModel convenio;
        private StatusPagamento status;

        public RelProcedimentosWin(DateTime dtInicial, DateTime dtFinal, ConvenioModel convenio, StatusPagamento status)
        {
            this.dtInicial = dtInicial;
            this.dtFinal = dtFinal;
            this.convenio = convenio;
            this.status = status;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Consultas.Procedimentos.rptProcedimentos.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportProcedimentos(dtInicial, dtFinal, convenio, status));
            rptViewer.LocalReport.SetParameters(ControleDataSets.GetParametros(dtInicial, dtFinal));

            rptViewer.RefreshReport();
        }
    }
}