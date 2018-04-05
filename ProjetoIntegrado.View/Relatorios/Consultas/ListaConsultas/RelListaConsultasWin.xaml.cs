using System;

namespace ProjetoIntegrado.View.Relatorios.Consultas.ListaConsultas
{
    using ViewUtil;
    using DataSets;
    using Model;

    public partial class RelListaConsultasWin
    {
        private DateTime dtInicial;
        private DateTime dtFinal;
        private FuncionarioModel medico;

        public RelListaConsultasWin(DateTime dtInicial, DateTime dtFinal, FuncionarioModel medico)
        {
            this.dtInicial = dtInicial;
            this.dtFinal = dtFinal;
            this.medico = medico;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Consultas.ListaConsultas.rptListaConsultas.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportListaDeConsultas(dtInicial, dtFinal, medico));
            rptViewer.LocalReport.SetParameters(ControleDataSets.GetParametros(dtInicial, dtFinal));

            rptViewer.RefreshReport();
        }
    }
}
