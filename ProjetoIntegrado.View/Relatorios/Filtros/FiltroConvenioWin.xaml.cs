using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Relatorios.Filtros
{
    using Model;
    using Funcoes;

    public partial class FiltroConvenioWin
    {
        #region PROPRIEDADES

        public bool SelecionouOK { get; private set; }
        public DateTime dtInicial => tbDataInicial.SelectedDate.Value;
        public DateTime dtFinal => tbDataFinal.SelectedDate.Value;
        public ConvenioModel convenio => cbConvenios.SelectedIndex == 0 ? null : convenios[cbConvenios.SelectedIndex - 1];

        public StatusPagamento status => (StatusPagamento)Enum.Parse(typeof(StatusPagamento), cbStatusPagamento.Text);

        private List<ConvenioModel> convenios;

        #endregion

        #region CTOR

        public FiltroConvenioWin()
        {
            InitializeComponent();
            CarregarConvenios();
            tbDataInicial.SelectedDate = DataUtil.GetPrimeiroDiaDesseMes();
            tbDataFinal.SelectedDate = DataUtil.GetUltimoDiaDesseMes();
        }

        private void CarregarConvenios()
        {
            convenios = ConvenioModel.CarregarTodos();

            cbConvenios.Items.Add("TODOS");
            convenios.ForEach(x => cbConvenios.Items.Add(x.nome));
        }

        #endregion

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
