namespace ProjetoIntegrado.Model
{
    public partial class MaterialModel
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; } = true;
    }
}
