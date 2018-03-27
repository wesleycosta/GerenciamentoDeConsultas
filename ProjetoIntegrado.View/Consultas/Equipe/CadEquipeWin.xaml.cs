using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ProjetoIntegrado.View.Consultas.Equipe
{
    using Model;
    using Funcoes;

    public partial class CadEquipeWin
    {
        #region PROPRIEDADES E CTOR

        public bool cadastrou { get; private set; }

        private List<FuncionarioModel> funcionarios = new List<FuncionarioModel>();
        private readonly CirurgiaModel cirurgia;

        public CadEquipeWin(CirurgiaModel cirurgia)
        {
            this.cirurgia = cirurgia;

            InitializeComponent();
            Iniciar();
        }

        #endregion

        #region CARREGAR E INICIAR

        private void Iniciar()
        {
            CarregarFuncionarios();
            Loaded += (o, a) => cbFuncionario.Focus();
        }

        private void CarregarFuncionarios()
        {
            funcionarios = FuncionarioModel.CarregarTodos();
            cbFuncionario.Items.Clear();
            funcionarios.ForEach(item => cbFuncionario.Items.Add(item.nome));

            if (funcionarios.Count > 0)
                cbFuncionario.SelectedIndex = 0;
        }


        #endregion

        #region MANTEM EQUIPE

        private EquipeCirurgiaModel ToModel() =>
            new EquipeCirurgiaModel
            {
                cirurgia = cirurgia,
                funcao = tbFuncao.Text,
                funcionario = funcionarios[cbFuncionario.SelectedIndex],
                ativo = true,
            };

        private void MantemCargo()
        {
            var equipe = ToModel();
            equipe.Cadastrar();
        }

        #endregion

        #region EVENTOS

        private void BtnSalvar_OnClick(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos.Validar(this))
            {
                MantemCargo();
                cadastrou = true;
                Close();
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e) => Close();

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void cbFuncionario_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbFuncionario.SelectedIndex >= 0)
                tbFuncao.Text = funcionarios[cbFuncionario.SelectedIndex].cargo.descricao;
        }

        #endregion
    }
}
