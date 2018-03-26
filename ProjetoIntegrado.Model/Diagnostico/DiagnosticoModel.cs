namespace ProjetoIntegrado.Model
{
    public partial class DiagnosticoModel
    {
        public int id { get; set; }
        public CategoriaModel categoria { get; set; }
        public decimal esferico { get; set; }
        public decimal cilindro { get; set; }
        public decimal adicao { get; set; }
        public decimal eixo { get; set; }
        public bool ativo { get; set; } = true;
    }
}
