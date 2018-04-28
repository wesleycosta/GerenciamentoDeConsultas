using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Consultas
{
    using Funcoes;
    using Mensagens;
    using Model;
    using System;
    using System.Collections.Generic;

    public partial class RemarcarConsultaWin
    {
        public bool OK { get; set; }

        private List<FuncionarioModel> oftals = new List<FuncionarioModel>();
        private ConsultaModel consulta;


        public RemarcarConsultaWin(ConsultaModel consulta)
        {
            InitializeComponent();
            this.consulta = consulta;
            Carregar();
        }

        private void Carregar()
        {
            tbData.SelectedDate = DateTime.Now.AddDays(1);
            CarregarOftals();

            tbData.Focus();
        }

        private void CarregarOftals()
        {
            cbMedicos.Items.Clear();
            oftals = FuncionarioModel.CarregarMedicos();

            foreach (var f in oftals)
                cbMedicos.Items.Add(f.nome);
        }

        private bool NaoExisteConsulta()
        {
            if (ConsultaModel.ExisteConsulta(tbData.SelectedDate.Value, TimeSpan.Parse(tbHorario.Text), consulta?.id ?? 0))
            {
                Mbox.Afirmacao("Aviso", "Já existe uma consulta agendada na data e horário informado!");
                return false;
            }

            return true;
        }

        private void MantemDados()
        {
            consulta.observacao = $"CONSULTA REMARCADA DE {consulta.data.ToShortDateString()} ÁS {consulta.horario.ToString(@"hh\:mm")} ";
            consulta.medico = oftals[cbMedicos.SelectedIndex];
            consulta.data = tbData.SelectedDate.Value;
            consulta.horario = TimeSpan.Parse(tbHorario.Text);
            consulta.observacao += $" PARA {consulta.data.ToShortDateString()} ÁS {consulta.horario.ToString(@"hh\:mm")}";
            consulta.ativo = true;

            consulta.Atualizar();
        }

        #region EVENTOS

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
                if (NaoExisteConsulta())
                {
                    MantemDados();
                    OK = true;
                    Close();
                }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e) => Close();


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
