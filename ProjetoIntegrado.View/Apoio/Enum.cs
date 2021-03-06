﻿namespace ProjetoIntegrado.View
{
    #region  ENUM MENU PRINCIPAL

    public enum MenuItensEnum
    {
        // PRINCIPAL
        Pacientes,
        Procedimentos,
        Convenio,

        // CADASTROS
        Empresa,
        Funcionarios,
        Usuarios,
        Cargos,
        Material,

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
        Procedimentos,

        // FINANCEIRO
        Despesas,
        Entradas,
        Faturamento
    }

    #endregion

}
