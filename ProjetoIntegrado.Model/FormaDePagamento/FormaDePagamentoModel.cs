namespace ProjetoIntegrado.Model
{
    public partial class FormaDePagamentoModel
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public bool ativo { get; set; } = true;
    }
}
