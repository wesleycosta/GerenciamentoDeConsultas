using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoIntegrado.View.Usuarios
{
    using Model;
    using Funcionario;
    using Mensagens;
    using ViewUtil;

    public partial class PrincipalUsuariosWin
    {
        private List<FuncionarioModel> lFuncionarios = new List<FuncionarioModel>();

        public PrincipalUsuariosWin()
        {
            InitializeComponent();

            Loaded += (o, a) =>
            {
                tbPesquisa.Focus();
                CarregarFuncionarios();
            };
        }

        #region EDITAR E CARREGAR

        private void Editar()
        {
            if (lvwCargos.SelectedIndex >= 0)
            {
                var funcionario = lvwCargos.SelectedItems[0] as FuncionarioModel;
                var cadFuncionario = new CadFuncionarioWin(funcionario);

                cadFuncionario.ShowDialog();

                if (cadFuncionario.cadastrou)
                    CarregarFuncionarios();
            }
            else
                Mbox.SelecioneUmaLinhaDaTabela();
        }

        private void CarregarFuncionarios()
        {
            if (string.IsNullOrEmpty(tbPesquisa.Text))
                lFuncionarios = FuncionarioModel.CarregarTodos();
            else
                lFuncionarios = FuncionarioModel.Pesquisar(FiltroPessoa.nome, tbPesquisa.Text);

            lvwCargos.ItemsSource = lFuncionarios;
            tbPesquisa.Focus();
            lbTotalRegistro.Content = lFuncionarios.Count.ToString("D2");
        }

        #endregion

        #region EVENTOS

        private void lvwCargos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Editar();
        }

        private void lvwCargos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Editar();
        }

        private void tbPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            CarregarFuncionarios();

            if (e.Key == Key.Down)
                lvwCargos.SelecionarPrimeiraLinha();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        #endregion
    }
}
