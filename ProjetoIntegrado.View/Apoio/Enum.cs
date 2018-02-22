namespace ProjetoIntegrado.View
{
    #region  ENUM MENU PRINCIPAL

    public enum MenuItensEnum
    {
        // PRINCIPAL
        Pacientes,
        Cirurgias,
        Convenio,

        // CADASTROS
        Empresa,
        Funcionarios,
        Usuarios,
        Cargos,

        // FINANCEIRO
        Despesas,
        FluxoDeCaixa,
        FormaDePagamento,

        // OUTROS
        Relatorios,
        TrocarUsuario
    }

    #endregion

    #region  ENUM MENU RELATÓRIOS

    enum RelatorioEnum
    {
        // CONSULTAS
        ListaDeConsultas,
        ConsultasCanceladas,

        // FINANCEIRO
        Faturamento
    }

    #endregion

}
