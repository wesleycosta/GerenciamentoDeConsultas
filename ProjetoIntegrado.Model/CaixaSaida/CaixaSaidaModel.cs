using System;

namespace ProjetoIntegrado.Model
{
    public partial class CaixaSaidaModel
    {
        public int id { get; set; }
        public int idCaixa { get; set; }
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public DateTime data { get; set; }
        public bool ativo { get; set; } = true;
    }
}
