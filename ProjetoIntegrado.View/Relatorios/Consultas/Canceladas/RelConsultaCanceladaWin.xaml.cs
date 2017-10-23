using System;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ProjetoIntegrado.View.Relatorios.Consultas.Canceladas
{
    using ViewUtil;
    using DataSets;
    using DataSets.dsEmpresaTableAdapters;

    public partial class RelConsultaCanceladaWin
    {
        public RelConsultaCanceladaWin()
        {
            InitializeComponent();
            rptViewer.FormatoImpressao();
        }

        private void RptViewer_OnLoad(object sender, EventArgs e)
        {
            var empresaDs = new dsEmpresa();
            var empresaTableAdapter = new empresaTableAdapter();

            var reportDs = new ReportDataSource
            {
                Name = "dsEmpresa",
                Value = new BindingSource
                {
                    DataMember = "empresa",
                    DataSource = empresaDs
                }
            };

            rptViewer.LocalReport.ReportEmbeddedResource = "ProjetoIntegrado.View.Relatorios.Consultas.Canceladas.rptConsultasCanceladas.rdlc";
            rptViewer.LocalReport.DataSources.Add(reportDs);
            empresaTableAdapter.Fill(empresaDs.empresa);

            rptViewer.RefreshReport();
        }
    }
}
