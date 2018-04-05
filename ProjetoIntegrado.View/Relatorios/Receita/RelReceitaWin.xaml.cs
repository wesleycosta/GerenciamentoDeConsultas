using System;

namespace ProjetoIntegrado.View.Relatorios.Receita
{
    using ViewUtil;
    using DataSets;

    public partial class RelReceitaWin 
    {
        private int id;

        public RelReceitaWin(int id)
        {
            this.id = id;

            InitializeComponent();
            rptViewer.FormatoImpressao();

            Loaded += (o, a) => SplashScreenControle.Fechar();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Receita.rptReceita.rdlc";

            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportEmpresa());
            rptViewer.LocalReport.DataSources.Add(ControleDataSets.GetReportReceita(id));

            rptViewer.RefreshReport();
        }
    }
}
