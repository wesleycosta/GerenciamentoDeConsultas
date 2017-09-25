using System.Drawing;

namespace ProjetoIntegrado.Model
{
    public partial class ClinicaModel
    {
        public int id { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string dddCel { get; set; }
        public string celular { get; set; }
        public string dddTel { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }

        public string site { get; set; }
        public Image logo { get; set; }
        public EnderecoModel endereco { get; set; }
    }
}
