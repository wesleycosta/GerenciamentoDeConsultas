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
                case MenuItensEnum.Pacientes: Pacientes(); break;
                case MenuItensEnum.Procedimentos: Procedimentos(); break;
                case MenuItensEnum.Convenio: Convenio(); break;

                case MenuItensEnum.Empresa: Empresa(); break;
                case MenuItensEnum.Funcionarios: Funcionarios(); break;
                case MenuItensEnum.Usuarios: Usuarios(); break;
                case MenuItensEnum.Cargos: Cargos(); break;

                case MenuItensEnum.Despesas: Despesas(); break;
                case MenuItensEnum.FluxoDeCaixa: FluxoDeCaixa(); break;
                case MenuItensEnum.FormaDePagamento: FormaDePagamento(); break;

                case MenuItensEnum.Relatorios: Relatorios(); break;
                case MenuItensEnum.TrocarUsuario: TrocarUsuario(); break;
            }
        }

        #endregion

        #region  PRINCIPAL

        private static void Pacientes() =>
            new Clientes.PrincipalClienteWin().ShowDialog();

        private static void Procedimentos()
        {
            //var c = new Model.ConsultaModel
            //{
            //    cliente = new Model.ClienteModel
            //    {
            //        id = 1,
            //    },
            //    medico = new Model.FuncionarioModel
            //    {
            //        id = 1,
            //    },
            //    convenio = new Model.ConvenioModel
            //    {
            //        id = 1
            //    },
            //    numeroProcedimento = "123",
            //    data = DateTime.Now,
            //    id = 1,
            //    ativo = true,
            //    formaDeAtentimento = Model.FormaDeAtendimento.Convenio,
            //    horario = DateTime.Now.TimeOfDay,
            //    statusPagamento = Model.StatusPagamento.Pendente,
            //    tipoDeCancelamento = Model.TipoDeCancelamento.NaoRealizado,
            //    valor = 200
            //};

            //c.Cadastrar();
        }

        private static void Convenio() => new Convenio.PrincipalConvenioWin().ShowDialog();


        #endregion

        #region CADASTROS

        private static void Empresa() => new Clinica.CadClinicaWin().ShowDialog();

        private static void Funcionarios() => new Funcionario.PrincipalFuncionarioWin().ShowDialog();

        private static void Usuarios() => new Usuarios.PrincipalUsuariosWin().ShowDialog();

        private static void Cargos() => new Cargo.PrincipalCargoWin().ShowDialog();

        private static void Categoria() => new Categoria.PrincipalCategoriaWin().ShowDialog();

        #endregion

        #region FINANCEIRO

        private static void Despesas() =>
            new Despesa.PrincipalDespesaWin().ShowDialog();

        private static void FluxoDeCaixa() =>
            new FluxoDeCaixa.PrincipalFluxoDeCaixaWin().ShowDialog();

        private static void FormaDePagamento() =>
            new FormaDePagamento.PrincipalFormaDePagamentoWin().ShowDialog();

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