using System;

namespace ProjetoIntegrado.View.Principal
{
    public static class MenuItens
    {
        public static void MantemItem(string itemSelecionado)
        {
            var item = (MenuItensEnum)Enum.Parse(typeof(MenuItensEnum), itemSelecionado);

            switch (item)
            {
                case MenuItensEnum.Agenda: Agenda(); break;
                case MenuItensEnum.ListaDeConsultas: ListaDeConsultas(); break;
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
                case MenuItensEnum.Configuracoes: Configuracoes(); break;
            }
        }

        #region MENU ITENS
        private static void Agenda() { }
        private static void ListaDeConsultas() { }
        private static void Pacientes() =>
            new WebCam.WebCamWin().ShowDialog();

        // CADASTROS
        private static void Empresa() { }
        private static void Funcionarios() =>
            new Funcionario.PrincipalFuncionarioWin().ShowDialog();
        private static void Usuarios() { }

        private static void Cargos() =>
            new Cargo.PrincipalCargoWin().ShowDialog();

        private static void Categoria() =>
            new Categoria.PrincipalCategoriaWin().ShowDialog();

        // FINANCEIRO
        private static void FormaDePagamento() =>
            new FormaDePagamento.PrincipalFormaDePagamentoWin().ShowDialog();

        private static void FluxoDeCaixa() { }
        private static void LivroCaixa() { }
        private static void Faturamento() { }

        // OUTROS
        private static void Relatorios() { }
        private static void Configuracoes() { }

        #endregion
    }
}