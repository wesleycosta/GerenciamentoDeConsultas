using System;

namespace ProjetoIntegrado.Model
{
    using Funcoes;

    public partial class FuncionarioModel : Pessoa
    {
        // INFORMAÇÕES TRABALHISTA
        public CargoModel cargo { get; set; }
        public DateTime dataDeAdmissao { get; set; }
        public decimal salario { get; set; }

        //  LOGIN
        public string usuario { get; set; }
        public string senha { get; set; }

        public string senhaHash { get; set; }
        public string senhaMd5() => string.IsNullOrEmpty(senha) ? senhaHash : MD5.Criptografar(senha);
    }
}
