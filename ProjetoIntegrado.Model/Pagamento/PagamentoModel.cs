using System;

namespace ProjetoIntegrado.Model
{
    public partial class PagamentoModel
    {
        public int id { get; set; }
        public CaixaModel caixa { get; set; }
        public FormaDePagamentoModel formaDePagamento { get; set; }
        public DateTime data { get; set; }
        public decimal valor { get; set; }
        public int qtdParcelas { get; set; }
        public bool ativo { get; set; } = true;
    }
}
