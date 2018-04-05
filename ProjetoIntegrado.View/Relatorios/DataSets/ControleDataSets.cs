using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Relatorios.DataSets
{
    using Model;

    using dsEmpresaTableAdapters;
    using dsConsultaTableAdapters;
    using dsDespesasTableAdapters;
    using dsFinanceiroTableAdapters;

    public class ControleDataSets
    {
        #region EMPRESA

        public static ReportDataSource GetReportEmpresa()
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

            empresaTableAdapter.Fill(empresaDs.empresa);

            return reportDs;
        }

        #endregion

        #region CONSULTAS CANCELADAS

        public static ReportDataSource GetReportConsultaCancelada(DateTime dtInicial, DateTime dtFinal)
        {
            var consultaDs = new dsConsulta();
            var consultaTableAdapter = new consultas_canceladasTableAdapter();

            var reportConsulta = new ReportDataSource
            {
                Name = "dsConsultasCanceladas",
                Value = new BindingSource
                {
                    DataMember = "consultas_canceladas",
                    DataSource = consultaDs
                }
            };

            consultaTableAdapter.Fill(consultaDs.consultas_canceladas, dtInicial.ToShortDateString(), dtFinal.ToShortDateString());

            return reportConsulta;
        }

        #endregion

        #region LISTA DE CONSULTAS

        public static ReportDataSource GetReportListaDeConsultas(DateTime dtInicial, DateTime dtFinal, FuncionarioModel medico)
        {
            var consultaDs = new dsConsulta();
            var consultaListaTableAdapter = new lista_consultasTableAdapter();

            var reportConsulta = new ReportDataSource
            {
                Name = "dsListaConsultas",
                Value = new BindingSource
                {
                    DataMember = "lista_consultas",
                    DataSource = consultaDs
                }
            };

            consultaListaTableAdapter.Fill(consultaDs.lista_consultas,
                                           dtInicial.ToShortDateString(),
                                           dtFinal.ToShortDateString(),
                                           medico == null ? 1 : 0,
                                           medico?.id ?? 0);

            return reportConsulta;
        }

        #endregion

        #region PROCEDIMENTOS

        public static ReportDataSource GetReportProcedimentos(DateTime dtInicial, DateTime dtFinal, ConvenioModel convenio, StatusPagamento status)
        {
            var consultaDs = new dsConsulta();
            var procedimentosTableAdapter = new procedimentosTableAdapter();

            var reportConsulta = new ReportDataSource
            {
                Name = "dsProcedimentos",
                Value = new BindingSource
                {
                    DataMember = "procedimentos",
                    DataSource = consultaDs
                }
            };

            procedimentosTableAdapter.Fill(consultaDs.procedimentos,
                                           ((int)status).ToString(),
                                           status == StatusPagamento.Todos ? 1 : 0,
                                           dtInicial.ToShortDateString(),
                                           dtFinal.ToShortDateString());
            return reportConsulta;
        }

        #endregion

        #region DESPESAS

        public static ReportDataSource GetReportDespesas(DateTime dtInicial, DateTime dtFinal)
        {
            var despesaDs = new dsDespesas();
            var despesaTableAdapter = new despesasTableAdapter();

            var reportConsulta = new ReportDataSource
            {
                Name = "dsDespesas",
                Value = new BindingSource
                {
                    DataMember = "despesas",
                    DataSource = despesaDs
                }
            };

            despesaTableAdapter.Fill(despesaDs.despesas,
                                    dtInicial.ToShortDateString(),
                                    dtFinal.ToShortDateString());

            return reportConsulta;
        }

        #endregion

        #region FECHAMENTO

        public static ReportDataSource GetReportFechamento(DateTime dtInicial, DateTime dtFinal)
        {
            var financeiroDs = new dsFinanceiro();
            var fechamentoTableAdapter = new fechamentoTableAdapter();

            var report = new ReportDataSource
            {
                Name = "dsFechamento",
                Value = new BindingSource
                {
                    DataMember = "fechamento",
                    DataSource = financeiroDs
                }
            };

            fechamentoTableAdapter.Fill(financeiroDs.fechamento,
                                        dtInicial.ToShortDateString(),
                                        dtFinal.ToShortDateString());

            return report;
        }

        #endregion

        #region RECEITA

        public static ReportDataSource GetReportReceita(int id)
        {
            var consultaDs = new dsConsulta();
            var receitaTableAdapter = new receitaTableAdapter();

            var reportDs = new ReportDataSource
            {
                Name = "dsReceita",
                Value = new BindingSource
                {
                    DataMember = "receita",
                    DataSource = consultaDs
                }
            };

            receitaTableAdapter.Fill(consultaDs.receita, id);

            return reportDs;
        }

        #endregion

        #region GET PERIODO

        public static List<ReportParameter> GetParametros(DateTime dtInicial, DateTime dtFinal) =>
            new List<ReportParameter>
            {
                new ReportParameter("data_inicial", dtInicial.ToShortDateString()),
                new ReportParameter("data_final", dtFinal.ToShortDateString()),
            };

        #endregion
    }
}
