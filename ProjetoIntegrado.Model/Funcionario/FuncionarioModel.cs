
using System;

namespace ProjetoIntegrado.Model
{
    using Funcoes;

    public partial class FuncionarioModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public Genero genero { get; set; }
        public DateTime dataDeNascimento { get; set; }

        // CONTATO
        public string dddTel { get; set; }
        public string telefone { get; set; }
        public string dddCel { get; set; }
        public string celular { get; set; }
        public string email { get; set; }

        // INFORMAÇÕES TRABALHISTA
        public CargoModel cargo { get; set; }
        public DateTime dataDeAdmissao { get; set; }
        public decimal salario { get; set; }

        //  LOGIN
        public string usuario { get; set; }
        public string senha { get; set; }
        public string senhaMd5() => MD5.Criptografar(senha);

        public EnderecoModel endereco { get; set; }
        public bool ativo { get; set; } = true;
    }
}
