using System;

namespace ProjetoIntegrado.View.Principal
{
    using Model;

    public class NivelAcesso
    {
        private CargoModel cargo;

        public NivelAcesso(CargoModel cargo)
        {
            this.cargo = cargo;
        }

        public bool AcessoProcedimentos => cargo?.IsGerente ?? false;
        public bool AcessoEmpresa => cargo?.IsGerente ?? false;

        public bool AcessoFuncionario => cargo?.IsGerente ?? false;
        public bool AcessoUsuarios=> cargo?.IsGerente ?? false;

        public bool AcessoFinanceiro => cargo?.IsGerente ?? false;
    }
}
