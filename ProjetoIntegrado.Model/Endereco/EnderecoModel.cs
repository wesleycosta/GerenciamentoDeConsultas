using Newtonsoft.Json;

namespace ProjetoIntegrado.Model
{
    public partial class EnderecoModel
    {
        public int id { get; set; }
        public string cep { get; set; }
        [JsonProperty("localidade")]
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public bool ativo { get; set; } = true;
    }
}
