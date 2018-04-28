using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Relatorios.Filtros
{
    using Model;
    using Funcoes;

    public partial class FiltroOftalWin
    {
        public bool SelecionouOK { get; private set; }
        public DateTime dtInicial => tbDataInicial.SelectedDate.Value;
        public DateTime dtFinal => tbDataFinal.SelectedDate.Value;
        public FuncionarioModel medico => cbMedicos.SelectedIndex == 0 ? null : medicos[cbMedicos.SelectedIndex - 1];

        private List<FuncionarioModel> medicos;

        public FiltroOftalWin()
        {
            InitializeComponent();

            CarregarMedicos();
            tbDataInicial.SelectedDate = DataUtil.GetPrimeiroDiaDesseMes();
            tbDataFinal.SelectedDate = DataUtil.GetUltimoDiaDesseMes();
        }

        private void CarregarMedicos()
        {
            medicos = FuncionarioModel.CarregarMedicos();

            cbMedicos.Items.Add("TODOS");

            medicos.ForEach(x => cbMedicos.Items.Add(x.nome));
        }

        #region EVENTOS

        private void BtnOk_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            SelecionouOK = true;
            Close();
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) => Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
