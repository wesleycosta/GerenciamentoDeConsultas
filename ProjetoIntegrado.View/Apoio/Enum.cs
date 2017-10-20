namespace ProjetoIntegrado.View
{
    #region  ENUM MENU PRINCIPAL

    enum MenuItensEnum
    {
        // CONSULTAS
        Agenda,
        Pacientes,

        // CADASTROS
        Empresa,
        Funcionarios,
        Usuarios,
        Cargos,
        Categoria,

        // FINANCEIRO
        FormaDePagamento,
        FluxoDeCaixa,
        LivroCaixa,
        Faturamento,

        // OUTROS
        Relatorios
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
