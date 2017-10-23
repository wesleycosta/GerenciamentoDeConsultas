using System;
using ProjetoIntegrado.Mensagens;

namespace ProjetoIntegrado.View.Principal
{
    public static class MenuItens
    {
        public static EventHandler Evento;

        #region MANTEM ITENS

        public static MenuItensEnum GetItem(string item)
        {
            return (MenuItensEnum)Enum.Parse(typeof(MenuItensEnum), item);
        }

        public static void MantemItem(string itemSelecionado)
        {
            var item = GetItem(itemSelecionado);

            switch (item)
            {
                case MenuItensEnum.Agenda: Agenda(); break;
                case MenuItensEnum.Pacientes: Pacientes(); break;

                case MenuItensEnum.Empresa: Empresa(); break;
                case MenuItensEnum.Funcionarios: Funcionarios(); break;
                case MenuItensEnum.Usuarios: Usuarios(); break;
                case MenuItensEnum.Cargos: Cargos(); break;
                case MenuItensEnum.Categoria: Categoria(); break;

                case MenuItensEnum.FormaDePagamento: FormaDePagamento(); break;
                case MenuItensEnum.FluxoDeCaixa: FluxoDeCaixa(); break;
                case MenuItensEnum.LivroCaixa: LivroCaixa(); break;
                case MenuItensEnum.Faturamento: Faturamento(); break;

                case MenuItensEnum.Relatorios: Relatorios(); break;
                case MenuItensEnum.TrocarUsuario: TrocarUsuario(); break;
            }
        }

        #endregion

        #region  PRINCIPAL

        private static void Agenda() { }

        private static void Pacientes() => new Clientes.PrincipalClienteWin().ShowDialog();

        #endregion

        #region CADASTROS

        private static void Empresa() => new Clinica.CadClinicaWin().ShowDialog();

        private static void Funcionarios() => new Funcionario.PrincipalFuncionarioWin().ShowDialog();

        private static void Usuarios() => new Usuarios.PrincipalUsuariosWin().ShowDialog();

        private static void Cargos() => new Cargo.PrincipalCargoWin().ShowDialog();

        private static void Categoria() => new Categoria.PrincipalCategoriaWin().ShowDialog();

        #endregion

        #region FINANCEIRO
        private static void FormaDePagamento() => new FormaDePagamento.PrincipalFormaDePagamentoWin().ShowDialog();

        private static void FluxoDeCaixa() => new FluxoDeCaixa.PrincipalFluxoDeCaixaWin().ShowDialog();

        private static void LivroCaixa() => new LivroCaixa.PrincipalLivroCaixa().ShowDialog();

        private static void Faturamento() => new WebCam.WebCamWin().ShowDialog();

        #endregion

        #region  OUTROS

        private static void Relatorios() => new Relatorios.RelatorioWins().ShowDialog();

        private static void TrocarUsuario()
        {
            new Login.LoginWin(null, true).ShowDialog();
            Evento(MenuItensEnum.TrocarUsuario, EventArgs.Empty);
        }

        #endregion
    }
}