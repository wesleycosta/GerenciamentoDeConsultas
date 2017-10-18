using System;

namespace ProjetoIntegrado.Model
{
    public abstract class Pessoa 
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

        public EnderecoModel endereco { get; set; }
        public bool ativo { get; set; } = true;
    }
}
